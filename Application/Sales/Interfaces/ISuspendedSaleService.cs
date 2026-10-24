using Application.Sales.Dtos;
using System.Collections.Generic;

namespace Application.Sales.Interfaces
{
    /// <summary>
    /// Guarda las ventas que la cajera dejó "en espera" para atender a otro cliente.
    ///
    /// Se registra como SINGLETON a propósito: FormAdminDashboard.OpenScreen hace Dispose del
    /// UserControl y del scope de DI en cada navegación, así que si este estado viviera dentro
    /// de UserControlNewSales se perdería apenas la cajera va a otra pantalla y vuelve.
    /// </summary>
    public interface ISuspendedSaleService
    {
        /// <summary>Tope de ventas abiertas a la vez, para que la barra de pestañas no se vuelva ilegible.</summary>
        int MaxOpenSales { get; }

        /// <summary>
        /// Cuál era la venta que la cajera estaba usando. Se guarda acá (y no en el UserControl)
        /// para poder volver a ella después de ir a otra pantalla o de reabrir el sistema.
        /// </summary>
        int? ActiveSaleId { get; set; }

        /// <summary>Ventas en espera, en el orden en que se abrieron (las nuevas van al final).</summary>
        IReadOnlyList<SuspendedSaleDto> GetAll();

        /// <summary>Crea una venta en espera vacía al final de la barra y la persiste.</summary>
        SuspendedSaleDto CreateNew();

        /// <summary>Actualiza una venta ya existente (o la agrega si no estaba) y persiste.</summary>
        void Save(SuspendedSaleDto sale);

        void Remove(int id);
    }
}
