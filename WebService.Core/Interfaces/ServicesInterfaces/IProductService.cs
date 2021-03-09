using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebService.Core.Entities;

namespace WebService.Core.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO> CreateProduct(Product Product);
        Task<List<ProductDTO>> GetAllProductsByCategoryId(string id);
        Task<ProductDTO> GetProductById(string id);
        Task<ProductDTO> UpdateProductById(ProductDTO product);
        Task<ProductDTO> DeleteProductById(string id);
    }
}
