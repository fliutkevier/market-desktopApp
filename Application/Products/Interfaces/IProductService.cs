using Application.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductGridDto>> GetAllForGridAsync();
        Task CreateProductAsync(CreateProductDto dto);
        Task<ProductDetailDto?> GetProductForEditAsync(string code);
        Task UpdateProductAsync(UpdateProductDto dto);
        Task SoftDeleteProductAsync(string code);
        Task<IEnumerable<ProductGridDto>> GetDeletedAsync();
        Task RestoreAsync(string code);
    }
}
