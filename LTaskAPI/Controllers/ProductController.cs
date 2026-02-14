using LTaskAPI.DTOs;
using LTaskAPI.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace LTaskAPI.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetAllProductsQueryDto productsQueryDto)
    {
        var products = await productService.GetAsync(productsQueryDto);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ResultDto<ProductDto>> Get([FromRoute] int id)
    {
        return await productService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<ResultDto<ProductDto>> Create([FromBody] CreateProductDto createProductDto)
    {
        return await productService.CreateAsync(createProductDto);
    }

    [HttpPut("{id}")]
    public async Task<ResultDto<ProductDto>> Update([FromRoute] int id, [FromBody] UpdateProductDto updateProductDto)
    {
        return await productService.UpdateAsync(id, updateProductDto);
    }

    [HttpDelete("{id}")]
    public async Task<ResultDto<bool>> Delete([FromRoute] int id)
    {
        return await productService.DeleteAsync(id);
    }

}
