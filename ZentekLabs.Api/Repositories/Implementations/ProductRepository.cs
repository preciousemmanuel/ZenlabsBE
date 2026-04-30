using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ZentekLabs.Data;
using ZentekLabs.Models.Domain;
using ZentekLabs.Models.Dtos.Request;
using ZentekLabs.Models.Dtos.Response;
using ZentekLabs.Repositories.Interfaces;

namespace ZentekLabs.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ZenteklabDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ProductRepository> logger;

        public ProductRepository(ZenteklabDbContext dbContext,
            IMapper mapper,
            ILogger<ProductRepository> logger
            )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
            // Initialize your data source here (e.g., in-memory list, database context, etc.)
        }


        public async Task<ResponseData<ProductDto>> AddProductAsync(Product product)
        {
            try
            {
                dbContext.Product.Add(product);
                await dbContext.SaveChangesAsync();
                
                var productDto = mapper.Map<ProductDto>(product);
                return new ResponseData<ProductDto>
                {
                    Sucesss = true,
                    Message = "Product added successfully.",
                    Data = productDto
                };
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed (e.g., log the error)
                logger.LogError(ex, $"An error occurred while adding the product: {ex.Message}");
                return new ResponseData<ProductDto>
                {
                    Sucesss = false,
                    Message = $"An error occurred while adding the product: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseData<PginatedResponse<ProductDto>>> GetAllProductsAsync(GetProductRequest request)
        {
            try
            {
                var query = dbContext.Product
        
              .OrderByDescending(x => x.CreatedAt)
              .AsQueryable();

                if (request.IsActive!=null)
                {
                    query = query.Where(e => e.IsActive == request.IsActive);
                }

                if (!string.IsNullOrEmpty(request.Search))
                {
                    query = query.Where(e =>
                    e.Name.Contains(request.Search));
                }

                if (request.FromDate.HasValue)
                {
                    query = query.Where(e => e.CreatedAt >= request.FromDate.Value);
                }

                if (request.ToDate.HasValue)
                {
                    query = query.Where(e => e.CreatedAt <= request.ToDate.Value);
                }

                var totalCount = await query.CountAsync();
               
                var page = request.Page <= 0 ? 1 : request.Page;
                var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
                var products = await query
      .Skip((int)((page - 1) * pageSize))
      .Take((int)pageSize)
      .ToListAsync();
                var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);


                var items = mapper.Map<List<ProductDto>>(products);

            return new ResponseData<PginatedResponse<ProductDto>>
            {
                Sucesss = true,
                Message = "Products retrieved successfully.",
                Data = new PginatedResponse<ProductDto>
                {
                    Items = items,
                    TotalCount = totalCount,
                    Page = (int)page,
                    PageSize = (int)pageSize,
                    TotalPages = totalPages

                }
            };

                

            }
            catch(Exception ex)
            {
                // Handle exceptions as needed (e.g., log the error)
                logger.LogError(ex, $"An error occurred while retrieving products: {ex.Message}");
                return new ResponseData<PginatedResponse<ProductDto>>
               {
                   Sucesss = false,
                   Message = $"An error occurred while retrieving products: {ex.Message}",
                   Data = null
               };
            }
        }

        public async Task<ResponseData<ProductDto?>> GetProductByIdAsync(Guid id)
        {
            try {
            var product = await dbContext.Product.FindAsync(id);
                if(product == null)
                {
                    return new ResponseData<ProductDto?>
                    {
                        Sucesss = false,
                        Message = "Product not found.",
                        Data = null
                    };
                }

                var productDto = mapper.Map<ProductDto>(product);

                return new ResponseData<ProductDto?>
                {
                    Sucesss = true,
                    Message = "Product retrieved successfully.",
                    Data = productDto
                };


            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"An error occurred while retrieving the product: {ex.Message}");
                return new ResponseData<ProductDto?>
                {
                    Sucesss = false,
                    Message = $"An error occurred while retrieving the product: {ex.Message}",
                    Data = null
                };
            }
            
        }
    }
}
