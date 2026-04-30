using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit;
using ZentekLabs.Data;
using ZentekLabs.Models.Domain;
using ZentekLabs.Models.Dtos.Request;
using ZentekLabs.Models.Dtos.Response;
using ZentekLabs.Repositories.Implementations;

public class ProductRepositoryTests
{
    private readonly ZenteklabDbContext _dbContext;

    private readonly IMapper _mapper;

    private readonly ILogger<ProductRepository> _logger;

    private readonly ProductRepository _repository;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ZenteklabDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ZenteklabDbContext(options);

        _mapper = A.Fake<IMapper>();

        _logger = A.Fake<ILogger<ProductRepository>>();

        _repository = new ProductRepository(
            _dbContext,
            _mapper,
            _logger);
    }

    [Fact]
    public async Task AddProductAsync_ShouldAddProductSuccessfully()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "iPhone 15",
            Colour = "Black",
            Price = 1200,
            Description= "The iPhone 15 is the latest flagship smartphone from Apple, featuring a sleek design, powerful performance, and advanced camera capabilities. With its A16 Bionic chip, it delivers lightning-fast speed and efficiency. The iPhone 15 boasts a stunning Super Retina XDR display, providing vibrant colors and sharp details for an immersive viewing experience. It also offers enhanced battery life, allowing users to stay connected throughout the day. The device runs on the latest iOS version, offering a seamless user interface and access to a wide range of apps and features. With its cutting-edge technology and premium build quality, the iPhone 15 is a top choice for tech enthusiasts and Apple fans alike.",
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Colour = product.Colour,
            Price = product.Price
        };

        A.CallTo(() => _mapper.Map<ProductDto>(product))
            .Returns(productDto);

        // Act
        var result = await _repository.AddProductAsync(product);

        // Assert
        result.Should().NotBeNull();

        result.Sucesss.Should().BeTrue();

        result.Data.Should().BeEquivalentTo(productDto);

        var savedProduct = await _dbContext.Product
            .FirstOrDefaultAsync(x => x.Id == product.Id);

        savedProduct.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldReturnPaginatedProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone",
                Colour = "Black",
                Price = 1000,
                Description ="Nice",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                Colour = "White",
                Description = "Nice",
                Price = 900,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        };

        await _dbContext.Product.AddRangeAsync(products);

        await _dbContext.SaveChangesAsync();

        var productDtos = products.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Colour = x.Colour,
            Price = x.Price
        }).ToList();

        A.CallTo(() => _mapper.Map<List<ProductDto>>(
                A<List<Product>>._))
            .Returns(productDtos);

        var request = new GetProductRequest
        {
            Page = 1,
            PageSize = 10
        };

        // Act
        var result = await _repository.GetAllProductsAsync(request);

        // Assert
        result.Should().NotBeNull();

        result.Sucesss.Should().BeTrue();

        result.Data.Should().NotBeNull();

        result.Data!.Items.Should().HaveCount(2);

        result.Data.TotalCount.Should().Be(2);
    }

    [Fact]
    public async Task GetAllProductsAsync_ShouldFilterBySearch()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone",
                Colour = "Black",
                Price = 1000,
                Description = "Nice",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung",
                Colour = "White",
                Description = "Nice",
                Price = 900,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            }
        };

        await _dbContext.Product.AddRangeAsync(products);

        await _dbContext.SaveChangesAsync();

        var filteredProducts = products
            .Where(x => x.Name.Contains("iPhone"))
            .ToList();

        var productDtos = filteredProducts.Select(x => new ProductDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Colour = x.Colour,
            Price = x.Price
        }).ToList();

        A.CallTo(() => _mapper.Map<List<ProductDto>>(
                A<List<Product>>._))
            .Returns(productDtos);

        var request = new GetProductRequest
        {
            Search = "iPhone",
            Page = 1,
            PageSize = 10
        };

        // Act
        var result = await _repository.GetAllProductsAsync(request);

        // Assert
        result.Sucesss.Should().BeTrue();

        result.Data!.Items.Should().HaveCount(1);

        result.Data.Items.First().Name.Should().Be("iPhone");
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnProduct()
    {
        // Arrange
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "MacBook",
            Colour = "Gray",
            Description = "Nide",
            Price = 2500,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _dbContext.Product.AddAsync(product);

        await _dbContext.SaveChangesAsync();

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Colour = product.Colour,
            Price = product.Price
        };

        A.CallTo(() => _mapper.Map<ProductDto>(product))
            .Returns(productDto);

        // Act
        var result = await _repository.GetProductByIdAsync(product.Id);

        // Assert
        result.Should().NotBeNull();

        result.Sucesss.Should().BeTrue();

        result.Data.Should().BeEquivalentTo(productDto);
    }

    [Fact]
    public async Task GetProductByIdAsync_ShouldReturnNotFound()
    {
        // Act
        var result = await _repository.GetProductByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().NotBeNull();

        result.Sucesss.Should().BeFalse();

        result.Message.Should().Be("Product not found.");
    }
}