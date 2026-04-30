using ZentekLabs.Models.Domain;
using ZentekLabs.Models.Dtos.Request;
using ZentekLabs.Models.Dtos.Response;

namespace ZentekLabs.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<ResponseData<PginatedResponse<ProductDto>>> GetAllProductsAsync(GetProductRequest request);

        Task<ResponseData<ProductDto>> GetProductByIdAsync(Guid id);

        Task<ResponseData<ProductDto>> AddProductAsync(Product product);

        //Task UpdateProductAsync(Product product);

        //Task DeleteProductAsync(Guid id);
    }
}
