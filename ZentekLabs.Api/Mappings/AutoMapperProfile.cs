using AutoMapper;
using ZentekLabs.Models.Domain;
using ZentekLabs.Models.Dtos.Request;
using ZentekLabs.Models.Dtos.Response;

namespace ZentekLabs.Mappings
{
    public class AutoMapperProfile:Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductRequestDto>().ReverseMap();
        }
    }
}
