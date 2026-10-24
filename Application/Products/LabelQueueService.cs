using Application.Products.Dtos;
using Application.Products.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products
{
    public class LabelQueueService : ILabelQueueService
    {
        private readonly List<ProductLabelDto> _queue = new List<ProductLabelDto>();

        public void AddToQueue(ProductLabelDto label)
        {
            _queue.Add(label);
        }

        public List<ProductLabelDto> GetQueue()
        {
            return _queue;
        }

        public void RemoveFromQueue(ProductLabelDto label)
        {
            _queue.Remove(label);
        }

        public void ClearQueue()
        {
            _queue.Clear();
        }
    }
}
