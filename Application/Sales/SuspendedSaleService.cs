using Application.Sales.Dtos;
using Application.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Application.Sales
{
    public class SuspendedSaleService : ISuspendedSaleService
    {
        public int MaxOpenSales => 6;

        private readonly object _lock = new object();
        private readonly string _filePath;
        private readonly List<SuspendedSaleDto> _sales = new List<SuspendedSaleDto>();
        private int? _activeSaleId;

        /// <summary>Lo que se guarda en disco: las ventas más cuál estaba abierta en pantalla.</summary>
        private class PersistedState
        {
            public int? ActiveSaleId { get; set; }
            public List<SuspendedSaleDto> Sales { get; set; } = new List<SuspendedSaleDto>();
        }

        public SuspendedSaleService()
        {
            // Misma carpeta que ya usa Program.cs para la base (ProgramData\VerticeApp): sobrevive
            // a reinstalar el ejecutable y es escribible sin permisos de administrador.
            string folder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "VerticeApp");

            _filePath = Path.Combine(folder, "ventas-en-espera.json");

            Load();
        }

        public int? ActiveSaleId
        {
            get { lock (_lock) { return _activeSaleId; } }
            set
            {
                lock (_lock)
                {
                    if (_activeSaleId == value) return;
                    _activeSaleId = value;
                    Persist();
                }
            }
        }

        public IReadOnlyList<SuspendedSaleDto> GetAll()
        {
            lock (_lock)
            {
                // Por orden de apertura: las ventas nuevas siempre van al final de la barra, y el
                // rótulo de cada pestaña es su posición, no su Id.
                return _sales.OrderBy(s => s.CreatedAt).ThenBy(s => s.Id).ToList();
            }
        }

        public SuspendedSaleDto CreateNew()
        {
            lock (_lock)
            {
                if (_sales.Count >= MaxOpenSales)
                    throw new InvalidOperationException($"No se pueden tener más de {MaxOpenSales} ventas abiertas al mismo tiempo.");

                // Id nuevo siempre, nunca se reutiliza: es la identidad con la que la pantalla
                // recuerda en qué venta estaba parada.
                int id = _sales.Count == 0 ? 1 : _sales.Max(s => s.Id) + 1;

                var sale = new SuspendedSaleDto
                {
                    Id = id,
                    CreatedAt = DateTime.Now
                };

                _sales.Add(sale);
                Persist();
                return sale;
            }
        }

        public void Save(SuspendedSaleDto sale)
        {
            if (sale == null) return;

            lock (_lock)
            {
                int index = _sales.FindIndex(s => s.Id == sale.Id);
                if (index >= 0)
                    _sales[index] = sale;
                else
                    _sales.Add(sale);

                Persist();
            }
        }

        public void Remove(int id)
        {
            lock (_lock)
            {
                _sales.RemoveAll(s => s.Id == id);
                if (_activeSaleId == id) _activeSaleId = null;
                Persist();
            }
        }

        private void Load()
        {
            // Si el archivo está corrupto, es de una versión vieja, o no se puede leer, se arranca
            // vacío. La pantalla de ventas es lo único que no puede dejar de funcionar: perder las
            // ventas en espera es molesto, no poder vender es fatal.
            try
            {
                if (!File.Exists(_filePath)) return;

                string json = File.ReadAllText(_filePath);
                if (string.IsNullOrWhiteSpace(json)) return;

                // Las primeras versiones guardaban un array pelado, sin la venta activa.
                if (json.TrimStart().StartsWith("["))
                {
                    var legacy = JsonSerializer.Deserialize<List<SuspendedSaleDto>>(json);
                    if (legacy != null) _sales.AddRange(legacy.Where(s => s != null));
                }
                else
                {
                    var state = JsonSerializer.Deserialize<PersistedState>(json);
                    if (state != null)
                    {
                        _sales.AddRange(state.Sales.Where(s => s != null));
                        _activeSaleId = state.ActiveSaleId;
                    }
                }

                RepararIds();
            }
            catch
            {
                _sales.Clear();
                _activeSaleId = null;
            }
        }

        /// <summary>
        /// Defensa contra un archivo editado a mano o de un formato viejo: sin Ids únicos, dos
        /// pestañas distintas apuntarían a la misma venta.
        /// </summary>
        private void RepararIds()
        {
            bool hayDuplicadosOVacios = _sales.Any(s => s.Id <= 0)
                || _sales.Select(s => s.Id).Distinct().Count() != _sales.Count;

            if (!hayDuplicadosOVacios) return;

            int siguiente = 1;
            foreach (var sale in _sales.OrderBy(s => s.CreatedAt).ToList())
                sale.Id = siguiente++;

            _activeSaleId = null;
        }

        private void Persist()
        {
            try
            {
                string? folder = Path.GetDirectoryName(_filePath);
                if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var state = new PersistedState { ActiveSaleId = _activeSaleId, Sales = _sales };
                string json = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                // Si no se puede escribir (disco lleno, permisos), las ventas siguen vivas en
                // memoria y la caja sigue funcionando; solo se pierde la persistencia.
            }
        }
    }
}
