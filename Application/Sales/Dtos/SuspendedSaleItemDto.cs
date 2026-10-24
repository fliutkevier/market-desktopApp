namespace Application.Sales.Dtos
{
    /// <summary>
    /// Un producto dentro de una venta en espera. Espejo de SaleItemViewModel (proyecto WinForms),
    /// que Application no puede referenciar.
    /// </summary>
    public class SuspendedSaleItemDto
    {
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;

        // Se guarda el precio del momento en que se escaneó: si alguien cambia el precio del
        // producto mientras la venta está en espera, se respeta el que se le cotizó al cliente.
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
