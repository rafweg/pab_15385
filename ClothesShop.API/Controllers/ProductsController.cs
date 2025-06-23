#nullable disable
using ClothesShop.Application.DTOs;
using ClothesShop.Application.Interfaces;
using ClothesShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothesShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _productService.GetAllAsync();
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category,
            Brand = p.Brand,
            Sizes = p.Sizes,
            Colors = p.Colors,
            ImageUrl = p.ImageUrl,
            StockQuantity = p.StockQuantity,
            IsActive = p.IsActive
        });

        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound($"Product with ID {id} not found.");

        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = product.Category,
            Brand = product.Brand,
            Sizes = product.Sizes,
            Colors = product.Colors,
            ImageUrl = product.ImageUrl,
            StockQuantity = product.StockQuantity,
            IsActive = product.IsActive
        };

        return Ok(productDto);
    }

    [HttpGet("category/{category}")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(string category)
    {
        var products = await _productService.GetProductsByCategoryAsync(category);
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Category = p.Category,
            Brand = p.Brand,
            Sizes = p.Sizes,
            Colors = p.Colors,
            ImageUrl = p.ImageUrl,
            StockQuantity = p.StockQuantity,
            IsActive = p.IsActive
        });

        return Ok(productDtos);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
    {
        var product = new Product
        {
            Name = createProductDto.Name,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            Category = createProductDto.Category,
            Brand = createProductDto.Brand,
            Sizes = createProductDto.Sizes,
            Colors = createProductDto.Colors,
            ImageUrl = createProductDto.ImageUrl,
            StockQuantity = createProductDto.StockQuantity,
            IsActive = true
        };

        var createdProduct = await _productService.CreateProductAsync(product);

        var productDto = new ProductDto
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            Category = createdProduct.Category,
            Brand = createdProduct.Brand,
            Sizes = createdProduct.Sizes,
            Colors = createdProduct.Colors,
            ImageUrl = createdProduct.ImageUrl,
            StockQuantity = createdProduct.StockQuantity,
            IsActive = createdProduct.IsActive
        };

        return CreatedAtAction(nameof(GetProduct), new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto updateProductDto)
    {
        var existingProduct = await _productService.GetProductByIdAsync(id);
        if (existingProduct == null) return NotFound($"Product with ID {id} not found.");

        existingProduct.Name = updateProductDto.Name;
        existingProduct.Description = updateProductDto.Description;
        existingProduct.Price = updateProductDto.Price;
        existingProduct.Category = updateProductDto.Category;
        existingProduct.Brand = updateProductDto.Brand;
        existingProduct.Sizes = updateProductDto.Sizes;
        existingProduct.Colors = updateProductDto.Colors;
        existingProduct.ImageUrl = updateProductDto.ImageUrl;
        existingProduct.StockQuantity = updateProductDto.StockQuantity;
        existingProduct.IsActive = updateProductDto.IsActive;

        await _productService.UpdateProductAsync(existingProduct);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var success = await _productService.DeleteProductAsync(id);
        if (!success) return NotFound($"Product with ID {id} not found.");

        return NoContent();
    }
}