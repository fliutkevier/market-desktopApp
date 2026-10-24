# Limpieza total de ventas

Borra todo rastro de las ventas de `VerticeDB`, dejando antes un backup:
- Ventas, detalle y facturas (`Sales`, `SaleDetails`, `FiscalDocuments`).
- Todas las cajas (`Sessions`), incluida la abierta. Al entrar de nuevo se abre una caja nueva.
- Los registros del log de ventas, facturas, cajas y tickets. El resto del log queda.
- Reinicia la numeración: la próxima venta y la próxima caja son la N° 1.

No toca productos, stock, medios de pago, usuarios, empleados ni proveedores.

## Pasos en la PC del cliente

1. **Cerrar Vertice** (la cola de facturación de ARCA corre en segundo plano mientras el sistema está abierto).
2. **Backup**: copiar esta carpeta a la PC y, desde PowerShell, en la carpeta:

   ```
   powershell -ExecutionPolicy Bypass -File .\1_Backup_Ventas.ps1
   ```

   Crea en el Escritorio `Backup_Ventas_Vertice_<fecha>` con:
   - `VerticeDB_antes_limpieza_ventas_<fecha>.bak`: la base completa, restaurable tal cual.
   - `Ventas.csv`, `VentasDetalle.csv`, `FacturasFiscales.csv`, `Cajas.csv`, `Logs.csv` (log completo): para abrir en Excel.

   Si avisa que hay facturas **pendientes** de ARCA, abrir Vertice y emitirlas antes de seguir.
3. Abrir `Ventas.csv` en Excel y confirmar que están todas.
4. **Copiar la carpeta del backup fuera de la PC** (pendrive / Drive).
5. Ejecutar `2_Borrar_Ventas.sql` (SSMS con F5, o sin SSMS):

   ```
   sqlcmd -S localhost\SQLEXPRESS -E -i 2_Borrar_Ventas.sql
   ```

   Se frena solo si no hay un `.bak` de las últimas 24 hs o si hay facturas pendientes.
   Corre en una transacción: si algo falla, no se borra nada.
6. Opcional: borrar `C:\ProgramData\VerticeApp\ventas-en-espera.json` (ventas en espera sin cobrar, no están en la base).

## Restaurar el backup (si alguna vez hace falta)

Para consultar las ventas viejas sin pisar la base actual, restaurar con otro nombre en SSMS:
clic derecho en *Databases* > *Restore Database...* > *Device* > elegir el `.bak` > cambiar el nombre de destino
(por ejemplo `VerticeDB_Historico`) y en *Files* tildar *Relocate all files*.

Restaurar encima de `VerticeDB` vuelve la base entera al momento del backup: se pierde todo lo cargado después
(ventas nuevas, cambios de stock y de productos).
