using BlazorEcommerceSharedLibrary.Models;
using BlazorEcommerceSharedLibrary.Responses;

namespace BlazorEcommerceSharedLibrary.Contracts
{
    public interface IProduct
    {
        Task<ServiceResponse> AddProduct(Product model);
        Task<List<Product>> GetProducts(bool featured = false);
    }
}
