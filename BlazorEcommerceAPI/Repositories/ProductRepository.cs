using BlazorEcommerceAPI.Data;
using BlazorEcommerceSharedLibrary.Contracts;
using BlazorEcommerceSharedLibrary.Models;
using BlazorEcommerceSharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerceAPI.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse> AddProduct(Product model)
        {
            if (model == null) return new ServiceResponse(false, "Model is null");
            var (flag, message) = await CheckName(model.Name);
            if (flag)
            {
                _context.Products.Add(model);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Saved");
            } else
            {
                return new ServiceResponse(flag, message);
            }
        }

        public async Task<List<Product>> GetProducts(bool featured = false)
        {
            if (featured)
            {
                return await _context.Products.Where(p => p.Featured).ToListAsync();
            } else
            {
                return await _context.Products.ToListAsync();
            }
        }

        private async Task<ServiceResponse> CheckName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x =>
             x.Name.ToLower() == name.ToLower()
            );
            return product is null
                ? new ServiceResponse(true, "")
                : new ServiceResponse(false, "Product already exist");
        }
    }
}
