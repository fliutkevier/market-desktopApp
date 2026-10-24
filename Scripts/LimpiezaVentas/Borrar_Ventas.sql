/*
    LIMPIEZA TOTAL DE VENTAS - VerticeDB
    ------------------------------------
    Borra todo rastro de las ventas:
      - FiscalDocuments, SaleDetails y Sales (en ese orden, por las FK).
      - Sessions: TODAS las cajas, incluida la abierta (al volver a entrar se abre una nueva).
      - AuditLogs de ventas, facturas, cajas y tickets ("VENTA ...", "FISCAL VENTA ...",
        "CAJA ...", "TICKET ...").
    Y reinicia la numeración: la próxima venta es la N° 1 y la próxima caja la N° 1.

    NO toca productos, stock, medios de pago, usuarios, empleados, proveedores ni el resto del log.

    ANTES DE CORRER:
      1. Cerrar el sistema Vertice (la cola de facturación corre en segundo plano).
      2. Correr 1_Backup_Ventas.ps1, abrir los CSV y confirmar que está todo.
      3. Sacar la carpeta del backup de la PC (pendrive / Drive).

    Si no hay un backup completo de la base de las últimas 24 hs, el script se frena solo.
    Todo corre en una transacción: si algo falla, no se borra nada.

    Desde SSMS: abrir y F5.   Sin SSMS:  sqlcmd -S localhost\SQLEXPRESS -E -i Borrar_Ventas.sql
*/
USE [VerticeDB];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

-- 1 = borra aunque haya facturas PENDIENTES de emitir en ARCA (esas ventas nunca se facturarían).
-- Las pendientes no están en ninguna carpeta: son filas de FiscalDocuments con Status = 0, y ya
-- quedaron respaldadas en el .bak y en FacturasFiscales.csv / Ventas.csv ("Factura PENDIENTE").
DECLARE @PermitirFacturasPendientes BIT = 1;

-- Control 1: backup completo reciente
DECLARE @Base SYSNAME = DB_NAME();
IF NOT EXISTS (
    SELECT 1 FROM msdb.dbo.backupset
    WHERE database_name = @Base
      AND type = 'D'
      AND backup_finish_date > DATEADD(HOUR, -24, GETDATE()))
BEGIN
    RAISERROR('No hay backup completo de %s en las últimas 24 hs. Correr 1_Backup_Ventas.ps1 primero.', 16, 1, @Base);
    RETURN;
END

-- Control 2: facturas pendientes de ARCA
DECLARE @Pendientes INT = (SELECT COUNT(*) FROM FiscalDocuments WHERE Status = 0);
IF @Pendientes > 0 AND @PermitirFacturasPendientes = 0
BEGIN
    RAISERROR('Hay %d factura(s) PENDIENTE(S) de emitir en ARCA. Emitirlas desde el sistema o poner @PermitirFacturasPendientes = 1.', 16, 1, @Pendientes);
    RETURN;
END

IF @Pendientes > 0
BEGIN
    PRINT '--- FACTURAS PENDIENTES QUE SE VAN A BORRAR ---';
    SELECT f.SaleId AS NroVenta, s.DateAndTime AS FechaHora, s.Total, f.AttemptCount AS Intentos, f.LastError AS UltimoError
    FROM FiscalDocuments f
    JOIN Sales s ON s.Id = f.SaleId
    WHERE f.Status = 0
    ORDER BY f.SaleId;
END

-- Mismo criterio para contar y para borrar los logs.
DECLARE @LogsDeVentas TABLE (Id INT PRIMARY KEY);
INSERT INTO @LogsDeVentas (Id)
SELECT Id FROM AuditLogs
WHERE Action LIKE 'VENTA %'
   OR Action LIKE 'FISCAL VENTA %'
   OR Action LIKE 'CAJA %'
   OR Action LIKE 'TICKET %';

PRINT '--- ANTES ---';
SELECT
    (SELECT COUNT(*) FROM Sales)           AS Ventas,
    (SELECT COUNT(*) FROM SaleDetails)     AS Detalles,
    (SELECT COUNT(*) FROM FiscalDocuments) AS Facturas,
    (SELECT COUNT(*) FROM Sessions)        AS Cajas,
    (SELECT COUNT(*) FROM @LogsDeVentas)   AS LogsABorrar,
    (SELECT COUNT(*) FROM AuditLogs)       AS LogsTotales,
    (SELECT ISNULL(SUM(Total), 0) FROM Sales) AS TotalVendido;

BEGIN TRY
    BEGIN TRANSACTION;

    DELETE FROM FiscalDocuments;
    DELETE FROM SaleDetails;
    DELETE FROM Sales;
    DELETE FROM Sessions;
    DELETE FROM AuditLogs WHERE Id IN (SELECT Id FROM @LogsDeVentas);

    -- RESEED 0 sobre una tabla vacía que ya tuvo filas: el próximo Id es 1.
    DBCC CHECKIDENT ('Sales', RESEED, 0)           WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('SaleDetails', RESEED, 0)     WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('FiscalDocuments', RESEED, 0) WITH NO_INFOMSGS;
    DBCC CHECKIDENT ('Sessions', RESEED, 0)        WITH NO_INFOMSGS;

    COMMIT TRANSACTION;
    PRINT 'Limpieza confirmada.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    PRINT 'ERROR: no se borró nada.';
    THROW;
END CATCH

PRINT '--- DESPUES ---';
SELECT
    (SELECT COUNT(*) FROM Sales)           AS Ventas,
    (SELECT COUNT(*) FROM SaleDetails)     AS Detalles,
    (SELECT COUNT(*) FROM FiscalDocuments) AS Facturas,
    (SELECT COUNT(*) FROM Sessions)        AS Cajas,
    (SELECT COUNT(*) FROM AuditLogs)       AS LogsRestantes;
