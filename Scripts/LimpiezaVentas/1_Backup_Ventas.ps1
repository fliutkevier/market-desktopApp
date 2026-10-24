<#
    BACKUP DE VENTAS ANTES DE LA LIMPIEZA
    -------------------------------------
    Genera, en una carpeta nueva del Escritorio:
      - VerticeDB_<fecha>.bak  -> copia COMPLETA de la base (se restaura tal cual con SSMS).
      - Ventas.csv             -> una fila por venta, con medio de pago, usuario y datos de factura.
      - VentasDetalle.csv      -> una fila por producto vendido.
      - FacturasFiscales.csv   -> la tabla FiscalDocuments completa (CAE, número, punto de venta...).
      - Cajas.csv              -> la tabla Sessions completa.
      - Logs.csv               -> la tabla AuditLogs completa (registro de actividad).

    Los CSV usan el separador de lista y el formato decimal de Windows de la PC donde se corre,
    así abren directo con doble clic en Excel sin columnas corridas ni decimales rotos.

    Uso (desde PowerShell, en la carpeta del script, con el sistema Vertice CERRADO):
        powershell -ExecutionPolicy Bypass -File .\1_Backup_Ventas.ps1

    Si la instancia o la base no son las de siempre:
        powershell -ExecutionPolicy Bypass -File .\1_Backup_Ventas.ps1 -Server "localhost\SQLEXPRESS" -Database "VerticeDB"
#>
param(
    [string]$Server = "localhost\SQLEXPRESS",
    [string]$Database = "VerticeDB",
    [string]$OutputRoot = [Environment]::GetFolderPath("Desktop")
)

$ErrorActionPreference = "Stop"

$stamp = Get-Date -Format "yyyyMMdd_HHmmss"
$outDir = Join-Path $OutputRoot "Backup_Ventas_Vertice_$stamp"
New-Item -ItemType Directory -Path $outDir | Out-Null

$culture = [System.Globalization.CultureInfo]::CurrentCulture
$sep = $culture.TextInfo.ListSeparator

$connString = "Server=$Server;Database=$Database;Integrated Security=True;TrustServerCertificate=True"
$conn = New-Object System.Data.SqlClient.SqlConnection $connString
$conn.Open()

function Invoke-Query([string]$sql) {
    $cmd = $conn.CreateCommand()
    $cmd.CommandText = $sql
    $cmd.CommandTimeout = 0
    $table = New-Object System.Data.DataTable
    $adapter = New-Object System.Data.SqlClient.SqlDataAdapter $cmd
    [void]$adapter.Fill($table)
    return ,$table
}

function Format-Cell($value) {
    if ($value -is [DBNull] -or $null -eq $value) { return "" }
    if ($value -is [DateTime]) { return $value.ToString("yyyy-MM-dd HH:mm:ss") }
    if ($value -is [decimal] -or $value -is [double]) { return $value.ToString($culture) }

    $text = [string]$value
    if ($text.Contains($sep) -or $text.Contains('"') -or $text.Contains("`n") -or $text.Contains("`r")) {
        return '"' + $text.Replace('"', '""') + '"'
    }
    return $text
}

function Export-Table([System.Data.DataTable]$table, [string]$fileName) {
    $path = Join-Path $outDir $fileName
    # UTF-8 con BOM: sin el BOM, Excel rompe las tildes y la ñ.
    $writer = New-Object System.IO.StreamWriter($path, $false, (New-Object System.Text.UTF8Encoding $true))
    try {
        $writer.WriteLine(($table.Columns | ForEach-Object { $_.ColumnName }) -join $sep)
        foreach ($row in $table.Rows) {
            $writer.WriteLine(($row.ItemArray | ForEach-Object { Format-Cell $_ }) -join $sep)
        }
    }
    finally {
        $writer.Dispose()
    }
    Write-Host ("  {0,-22} {1,8} filas" -f $fileName, $table.Rows.Count)
    return $table.Rows.Count
}

try {
    # ---------------------------------------------------------------- 1) .bak completo
    Write-Host "Generando backup completo de la base (.bak)..."

    # El .bak lo escribe el servicio de SQL Server, no este usuario. Se le da permiso de escritura
    # sobre la carpeta del backup (la creamos nosotros, así que no hace falta ser administrador).
    # Si eso falla, se usa la carpeta de backups de la instancia, donde el servicio siempre puede.
    $bakName = "${Database}_antes_limpieza_ventas_$stamp.bak"

    function Backup-To([string]$path) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandTimeout = 0
        $cmd.CommandText = "BACKUP DATABASE [$Database] TO DISK = @path WITH COPY_ONLY, INIT, CHECKSUM, NAME = N'Antes de limpieza de ventas'"
        [void]$cmd.Parameters.AddWithValue("@path", $path)
        [void]$cmd.ExecuteNonQuery()

        $cmd = $conn.CreateCommand()
        $cmd.CommandTimeout = 0
        $cmd.CommandText = "RESTORE VERIFYONLY FROM DISK = @path WITH CHECKSUM"
        [void]$cmd.Parameters.AddWithValue("@path", $path)
        [void]$cmd.ExecuteNonQuery()
    }

    $bakPath = Join-Path $outDir $bakName
    try {
        $svc = (Invoke-Query "SELECT TOP 1 service_account AS A FROM sys.dm_server_services WHERE servicename LIKE 'SQL Server (%'").Rows[0].A
        if ($svc -and -not ($svc -is [DBNull])) {
            & icacls $outDir /grant "${svc}:(OI)(CI)M" | Out-Null
        }
        Backup-To $bakPath
    }
    catch {
        $backupDir = (Invoke-Query "SELECT CAST(SERVERPROPERTY('InstanceDefaultBackupPath') AS nvarchar(4000)) AS P").Rows[0].P
        if ($backupDir -is [DBNull] -or [string]::IsNullOrWhiteSpace($backupDir)) {
            throw "No se pudo hacer el .bak ($($_.Exception.Message)). Hacerlo a mano desde SSMS: clic derecho en la base > Tasks > Back Up."
        }
        $bakPath = Join-Path $backupDir $bakName
        Backup-To $bakPath
        Write-Warning "El .bak no se pudo guardar junto a los CSV; quedó en la carpeta de backups de SQL Server: $bakPath"
    }
    Write-Host "  Backup verificado: $bakPath"

    # ---------------------------------------------------------------- 2) CSV legibles
    Write-Host ""
    Write-Host "Exportando CSV a $outDir ..."

    $ventas = Invoke-Query @"
SELECT
    s.Id                    AS NroVenta,
    s.DateAndTime           AS FechaHora,
    s.Total,
    pm.Name                 AS MedioDePago,
    s.SessionId             AS NroCaja,
    se.Username             AS Usuario,
    (SELECT COUNT(*) FROM SaleDetails d WHERE d.SaleId = s.Id)              AS Renglones,
    (SELECT ISNULL(SUM(d.Quantity), 0) FROM SaleDetails d WHERE d.SaleId = s.Id) AS Unidades,
    CASE f.Status WHEN 1 THEN 'Facturada' WHEN 0 THEN 'Factura PENDIENTE' ELSE 'Sin factura' END AS EstadoFiscal,
    f.IssuerCuit            AS CuitEmisor,
    f.PointOfSale           AS PuntoVenta,
    f.InvoiceType           AS TipoComprobante,
    f.InvoiceNumber         AS NroComprobante,
    f.CAE,
    f.CAEExpirationDate     AS VencimientoCAE
FROM Sales s
LEFT JOIN PaymentMethods pm ON pm.Id = s.PaymentMethodId
LEFT JOIN Sessions se       ON se.Id = s.SessionId
OUTER APPLY (SELECT TOP 1 * FROM FiscalDocuments fd WHERE fd.SaleId = s.Id ORDER BY fd.Id DESC) f
ORDER BY s.Id
"@

    $detalle = Invoke-Query @"
SELECT
    d.SaleId        AS NroVenta,
    s.DateAndTime   AS FechaHora,
    d.ProductCode   AS Codigo,
    d.ProductName   AS Producto,
    d.Quantity      AS Cantidad,
    d.UnitPrice     AS PrecioUnitario,
    d.SubTotal,
    pm.Name         AS MedioDePago
FROM SaleDetails d
JOIN Sales s                ON s.Id = d.SaleId
LEFT JOIN PaymentMethods pm ON pm.Id = s.PaymentMethodId
ORDER BY d.SaleId, d.Id
"@

    $fiscales = Invoke-Query "SELECT * FROM FiscalDocuments ORDER BY Id"
    $cajas    = Invoke-Query "SELECT * FROM Sessions ORDER BY Id"
    # El log completo, no solo el de ventas: así queda la secuencia entera de lo que pasó.
    $logs     = Invoke-Query "SELECT * FROM AuditLogs ORDER BY Id"

    $nVentas = Export-Table $ventas   "Ventas.csv"
    [void](Export-Table $detalle  "VentasDetalle.csv")
    [void](Export-Table $fiscales "FacturasFiscales.csv")
    [void](Export-Table $cajas    "Cajas.csv")
    [void](Export-Table $logs     "Logs.csv")

    $resumen = Invoke-Query "SELECT COUNT(*) AS Cant, ISNULL(SUM(Total),0) AS Total, MIN(DateAndTime) AS Desde, MAX(DateAndTime) AS Hasta FROM Sales"
    $r = $resumen.Rows[0]
    Write-Host ""
    Write-Host "Resumen de lo respaldado:"
    Write-Host ("  Ventas: {0}   Total: {1}   Desde: {2}   Hasta: {3}" -f $r.Cant, $r.Total.ToString("N2", $culture), (Format-Cell $r.Desde), (Format-Cell $r.Hasta))

    if ($nVentas -ne [int]$r.Cant) {
        throw "La cantidad exportada ($nVentas) no coincide con la cantidad en la base ($($r.Cant)). NO borrar nada."
    }

    $pend = (Invoke-Query "SELECT COUNT(*) AS N FROM FiscalDocuments WHERE Status = 0").Rows[0].N
    if ($pend -gt 0) {
        Write-Warning "Hay $pend factura(s) PENDIENTE(S) de emitir en ARCA. Si se borran, esas ventas nunca se van a facturar. Revisarlas antes de limpiar."
    }

    Write-Host ""
    Write-Host "LISTO. Backup en: $outDir" -ForegroundColor Green
    Write-Host "Abrir Ventas.csv en Excel para comprobar antes de correr 2_Borrar_Ventas.sql."
}
finally {
    $conn.Dispose()
}
