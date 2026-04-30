using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using ZentekLabs.Controllers;
using ZentekLabs.Models.Dtos.Request;
using ZentekLabs.Models.Dtos.Response;
using ZentekLabs.Repositories.Interfaces;

namespace ZentekLabs.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly IProductRepository productRepository;

        private readonly ProductController _controller;
        private readonly IMapper mapper;

        public ProductControllerTests()
        {
            productRepository = A.Fake<IProductRepository>();

            mapper = A.Fake<IMapper>();

            _controller = new ProductController(productRepository,mapper);
        }

        [Fact]
        public async Task ProductController_GetAllProducts_ShouldReturnOk()
        {
            // Arrange
            var request = new GetProductRequest
            {
                Page = 1,
                PageSize = 10,
                Color = "Black"
            };

            var response = new ResponseData<PginatedResponse<ProductDto>>
            {
                Sucesss = true,
                Message = "Products retrieved successfully",
                Data = new PginatedResponse<ProductDto>
                {
                    Page = 1,
                    PageSize = 10,
                    TotalCount = 1,
                    TotalPages = 1,
                    Items = new List<ProductDto>
                {
                    new ProductDto
                    {
                        Name = "iPhone",
                        Colour = "Black",
                        Price = 1200
                    }
                }
                }
            };

            A.CallTo(() => productRepository.GetAllProductsAsync(
                    A<GetProductRequest>._))
                .Returns(response);

            // Act
            var result = await _controller.GetAllProducts(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult!.Value.Should()
                .BeEquivalentTo(response);
        }
    }
}
