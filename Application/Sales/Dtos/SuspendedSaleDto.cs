using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Application.Sales.Dtos
{
    /// <summary>
    /// Una venta a medio cargar que quedó "en espera" (el cliente fue a buscar otra cosa, no le
    /// anda la app de pago, etc.) mientras la cajera atiende al siguiente.
    /// </summary>
    public class SuspendedSaleDto
    {
        /// <summary>
        /// Identidad interna y estable de la venta. NO es lo que se muestra en la pestaña: el
        /// rótulo ("Venta 1", "Venta 2") es la posición en la barra, así que al cerrar una venta
        /// las demás se renumeran solas y siempre se leen 1, 2, 3 de izquierda a derecha.
        /// El nombre en el JSON queda como "Number" para no romper las ventas ya guardadas.
        /// </summary>
        [JsonPropertyName("Number")]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int? PaymentMethodId { get; set; }

        /// <summary>Efectivo recibido, tal cual lo tipeó la cajera (puede estar a medio escribir).</summary>
        public string CashText { get; set; } = string.Empty;

        public List<SuspendedSaleItemDto> Items { get; set; } = new List<SuspendedSaleItemDto>();

        [JsonIgnore]
        public decimal SubTotal => Items.Sum(x => x.UnitPrice * x.Quantity);

        [JsonIgnore]
        public bool IsEmpty => Items.Count == 0;
    }
}
