using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> CreateProductAsync(ProductDTO? productDto)
        {
            if (productDto is  null)
            {
                return "Produto Criado com Sucesso";
            }

            var produto = new Product( Guid.NewGuid(), productDto.Name, productDto.Price);

            await _repository.AddAsync(produto);

            return produto.Name;
           
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
