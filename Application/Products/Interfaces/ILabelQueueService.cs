using Application.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Interfaces
{
    public interface ILabelQueueService
    {
        void AddToQueue(ProductLabelDto label);
        List<ProductLabelDto> GetQueue();
        void ClearQueue();
        void RemoveFromQueue(ProductLabelDto label);
    }
}
