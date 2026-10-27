<#
    PUBLICAR UNA VERSIÓN NUEVA DE VERTICE POS
    -----------------------------------------
    1. Lee la versión de Vertice\WinForms.csproj (<Version>). Subirla ANTES de correr esto.
    2. Publica el proyecto WinForms en Release (carpeta publish limpia) y el servidor del
       celular (proyecto Web) en la subcarpeta Api.
    3. Arma el instalador con Inno Setup (Instalador\VerticePOS.iss).
    4. Crea el Release "vX.Y.Z" en GitHub con el instalador adjunto. Desde ese momento, el
       botón "Buscar actualizaciones" de Ajustes lo encuentra.

    Antes de publicar: hacer commit y push del código de esa versión (el tag se crea sobre main).

    Uso:
        powershell -ExecutionPolicy Bypass -File .\Instalador\publicar.ps1 -Notas "Qué cambió en esta versión"
        powershell -ExecutionPolicy Bypass -File .\Instalador\publicar.ps1 -NotasArchivo .\notas.txt
        powershell -ExecutionPolicy Bypass -File .\Instalador\publicar.ps1 -SoloArmar     (arma el instalador sin subirlo)

    Token de GitHub (una sola vez): crear un "fine-grained personal access token" con acceso solo
    al repo market-desktopApp y permiso "Contents: Read and write". Guardarlo en token.txt en la
    carpeta que contiene a la solución, en %USERPROFILE%\.vertice\github-token.txt o en la variable
    de entorno VERTICE_GITHUB_TOKEN. NUNCA dentro del repo.
    Sin token, el script arma el instalador y muestra cómo subirlo a mano desde la web.
#>
param(
    [string]$Notas = "",
    [string]$NotasArchivo = "",
    [switch]$SoloArmar,
    [string]$Repo = "fliutkevier/market-desktopApp"
)

$ErrorActionPreference = "Stop"
$root = Split-Path $PSScriptRoot -Parent
Set-Location $root

function Paso($texto) { Write-Host ""; Write-Host "== $texto" -ForegroundColor Cyan }

# ------------------------------------------------------------------ versión y notas
[xml]$csproj = Get-Content "Vertice\WinForms.csproj" -Raw
$version = ($csproj.Project.PropertyGroup | ForEach-Object { $_.Version } | Where-Object { $_ }) | Select-Object -First 1
if (-not $version -or $version -notmatch '^\d+\.\d+\.\d+$') { throw "No se encontró <Version>X.Y.Z</Version> en Vertice\WinForms.csproj." }
$tag = "v$version"

if ($NotasArchivo) { $Notas = Get-Content $NotasArchivo -Raw -Encoding UTF8 }
if (-not $SoloArmar -and [string]::IsNullOrWhiteSpace($Notas)) {
    throw "Faltan las notas de la versión (-Notas ""..."" o -NotasArchivo archivo.txt). Las ve el cliente antes de instalar."
}

# Última migración de base de esta versión: el actualizador la usa para saber si se puede
# volver a esta versión más adelante sin romper la base.
$migracion = Get-ChildItem "Infrastructure\Migrations\*.cs" |
    Where-Object { $_.Name -notlike "*.Designer.cs" -and $_.Name -ne "AppDbContextModelSnapshot.cs" } |
    Sort-Object Name | Select-Object -Last 1 | ForEach-Object { $_.BaseName }

Write-Host "Versión:          $version"
Write-Host "Última migración: $migracion"

# ------------------------------------------------------------------ token (antes de compilar, para avisar temprano)
$token = $env:VERTICE_GITHUB_TOKEN
if (-not $token) {
    # token.txt en la carpeta que contiene a la solución (fuera del repo) o en el perfil del usuario.
    $tokenFiles = @((Join-Path (Split-Path $root -Parent) "token.txt"), (Join-Path $env:USERPROFILE ".vertice\github-token.txt"))
    $tokenFile = $tokenFiles | Where-Object { Test-Path $_ } | Select-Object -First 1
    if ($tokenFile) { $token = (Get-Content $tokenFile -Raw).Trim() }
}

$headers = $null
if (-not $SoloArmar -and $token) {
    $headers = @{ Authorization = "Bearer $token"; Accept = "application/vnd.github+json"; "User-Agent" = "VerticePOS-Publicar"; "X-GitHub-Api-Version" = "2022-11-28" }
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

    $existe = $true
    try { Invoke-RestMethod -Headers $headers -Uri "https://api.github.com/repos/$Repo/releases/tags/$tag" | Out-Null }
    catch { if ($_.Exception.Response.StatusCode.value__ -eq 404) { $existe = $false } else { throw } }
    if ($existe) { throw "Ya existe el release $tag en GitHub. Subir <Version> en Vertice\WinForms.csproj." }
}

# ------------------------------------------------------------------ publicar
Paso "Publicando WinForms en Release"
$dotnet = (Get-Command dotnet -ErrorAction SilentlyContinue).Source
if (-not $dotnet) { $dotnet = "C:\Program Files\dotnet\dotnet.exe" }

$pub = "Vertice\bin\Release\net8.0-windows\publish\win-x64"
# Carpeta limpia: si quedaran archivos de publicaciones viejas, terminarían en el instalador.
if (Test-Path $pub) { Remove-Item $pub -Recurse -Force }

& $dotnet publish "Vertice\WinForms.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o $pub -nologo -v q
if ($LASTEXITCODE -ne 0) { throw "Falló dotnet publish." }

# Servidor de la app del celular (proyecto Web -> VerticeApi.exe). Va dentro de la carpeta del
# sistema, en Api\, para que se actualice junto con Vertice y nunca quede desfasado de la base.
Paso "Publicando el servidor del celular (Web)"
& $dotnet publish "Web\Web.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -o "$pub\Api" -nologo -v q
if ($LASTEXITCODE -ne 0) { throw "Falló dotnet publish del servidor del celular." }
if (-not (Test-Path "$pub\Api\VerticeApi.exe")) { throw "No se encontró Api\VerticeApi.exe en la carpeta publish." }

$claves = Get-ChildItem $pub -Recurse -File -Include *.key, *.pfx, *.p12, appsettings.local.json
if ($claves) { throw "La carpeta publish tiene archivos que no pueden ir en el instalador: $($claves.Name -join ', ')" }

# ------------------------------------------------------------------ instalador
Paso "Armando el instalador con Inno Setup"
$iscc = @("$env:LOCALAPPDATA\Programs\Inno Setup 6\ISCC.exe", "C:\Program Files (x86)\Inno Setup 6\ISCC.exe", "C:\Program Files\Inno Setup 6\ISCC.exe") |
    Where-Object { Test-Path $_ } | Select-Object -First 1
if (-not $iscc) { throw "No se encontró Inno Setup 6 (ISCC.exe)." }

& $iscc /Q "Instalador\VerticePOS.iss"
if ($LASTEXITCODE -ne 0) { throw "Falló la compilación del instalador." }

$installer = Get-Item "Instalador\Output\Instalador_VerticePOS_$version.exe"
$sha = (Get-FileHash $installer.FullName -Algorithm SHA256).Hash
Write-Host "Instalador: $($installer.FullName)"
Write-Host ("Tamaño:     {0:N1} MB" -f ($installer.Length / 1MB))
Write-Host "SHA-256:    $sha"

$body = "$($Notas.Trim())`n`n<!-- migracion: $migracion -->"

if ($SoloArmar) {
    Write-Host ""; Write-Host "Instalador armado (no se subió nada)." -ForegroundColor Green
    exit 0
}

if (-not $headers) {
    Write-Host ""
    Write-Host "No hay token de GitHub: subirlo a mano en https://github.com/$Repo/releases/new" -ForegroundColor Yellow
    Write-Host "  Tag:     $tag   (target: main)"
    Write-Host "  Título:  Vertice POS $version"
    Write-Host "  Archivo: $($installer.FullName)"
    Write-Host "  Descripción (copiar tal cual, incluida la última línea):"
    Write-Host ""
    Write-Host $body
    exit 0
}

# ------------------------------------------------------------------ GitHub Release
Paso "Creando el release $tag en GitHub"
$payload = @{ tag_name = $tag; target_commitish = "main"; name = "Vertice POS $version"; body = $body; draft = $false; prerelease = $false } | ConvertTo-Json
$release = Invoke-RestMethod -Method Post -Headers $headers -Uri "https://api.github.com/repos/$Repo/releases" `
    -Body ([Text.Encoding]::UTF8.GetBytes($payload)) -ContentType "application/json; charset=utf-8"

Paso "Subiendo el instalador ($([math]::Round($installer.Length / 1MB)) MB, puede tardar)"
try {
    $uploadUrl = "https://uploads.github.com/repos/$Repo/releases/$($release.id)/assets?name=$($installer.Name)"
    $asset = Invoke-RestMethod -Method Post -Headers $headers -Uri $uploadUrl -InFile $installer.FullName -ContentType "application/octet-stream" -TimeoutSec 1800
}
catch {
    # Un release sin instalador no le sirve a nadie: se borra para poder reintentar con la misma versión.
    $detalle = $_.Exception.Message
    try { Invoke-RestMethod -Method Delete -Headers $headers -Uri "https://api.github.com/repos/$Repo/releases/$($release.id)" | Out-Null } catch { }
    try { Invoke-RestMethod -Method Delete -Headers $headers -Uri "https://api.github.com/repos/$Repo/git/refs/tags/$tag" | Out-Null } catch { }
    throw "Falló la subida del instalador; se deshizo el release. Detalle: $detalle"
}

Write-Host ""
Write-Host "LISTO: Vertice POS $version publicado." -ForegroundColor Green
Write-Host $release.html_url
