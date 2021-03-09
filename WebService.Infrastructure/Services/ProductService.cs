using Mapster;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Core.Entities;
using WebService.Core.Interfaces;

namespace WebService.Infrastructure.Repositories
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ProductDTO> CreateProduct(Product product)
        {
            if (await _categoryRepository.GetCategoryById(product.CategoryId.ToString()) is null)
                throw new ArgumentNullException($"The category: {product.CategoryId} it doesn't exist");
            return await _productRepository.CreateProduct(product.Adapt<ProductDTO>());
        }

        public async Task<List<ProductDTO>> GetAllProductsByCategoryId(string id)
        {
            return await _productRepository.GetAllProductsByCategoryId(id);
        }

        public async Task<ProductDTO> GetProductById(string id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task<ProductDTO> UpdateProductById(ProductDTO product)
        {
            if (await _categoryRepository.GetCategoryById(product.CategoryId.ToString()) is null)
                throw new ArgumentNullException($"The category: {product.CategoryId} it doesn't exist");
            return await _productRepository.UpdateProductById(product);
        }

        public async Task<ProductDTO> DeleteProductById(string id)
        {
            return await _productRepository.DeleteProductById(id);
        }
    }
}
