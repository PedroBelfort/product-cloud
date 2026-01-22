using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext productContext;

        public ProductRepository(ProductContext productContext)
        {
            this.productContext = productContext;
        }



        public async Task<Product> AddAsync(Product? product)
        {
            productContext.Products.Add(product);
            await productContext.SaveChangesAsync();
            return product;
        }

 

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await productContext.Products.AsNoTracking().ToListAsync();
        }
    }
}
