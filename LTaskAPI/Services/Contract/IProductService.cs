using LTaskAPI.DTOs;

namespace LTaskAPI.Services.Contract;

public interface IProductService
{
    Task<ResultDto<PaginatedResponse<ProductDto>>> GetAsync(GetAllProductsQueryDto productsQueryDto);
    Task<ResultDto<ProductDto>> GetByIdAsync(int id);
    Task<ResultDto<ProductDto>> CreateAsync(CreateProductDto createProductDto);
    Task<ResultDto<ProductDto>> UpdateAsync(int id, UpdateProductDto updateProductDto);
    Task<ResultDto<bool>> DeleteAsync(int id);
}
