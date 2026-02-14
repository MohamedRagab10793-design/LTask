using AutoMapper;
using LTaskAPI.Data;
using LTaskAPI.Data.Entities;
using LTaskAPI.DTOs;
using LTaskAPI.Services.Contract;
using LTaskAPI.Validators;
using Microsoft.EntityFrameworkCore;

namespace LTaskAPI.Services.Implementation;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProductService> _logger;
    private readonly IMapper _mapper;
    public ProductService(
        ApplicationDbContext context,
        ILogger<ProductService> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ResultDto<PaginatedResponse<ProductDto>>> GetAsync(GetAllProductsQueryDto productsQueryDto)
    {
        productsQueryDto.PageSize = Math.Max(1, Math.Min(100, productsQueryDto.PageSize));
        var query = _context.Products.AsNoTracking().OrderByDescending(w => w.CreateDate).AsQueryable();
        if (!string.IsNullOrEmpty(productsQueryDto.SearchText))
            query = query.Where(w => w.Name.Contains(productsQueryDto.SearchText));

        var totalCount = await query.CountAsync();
        var items = await _mapper.ProjectTo<ProductDto>(query.Skip((productsQueryDto.PageNumber - 1) * productsQueryDto.PageSize).Take(productsQueryDto.PageSize))
                                 .ToListAsync(CancellationToken.None);

        var resultData = new PaginatedResponse<ProductDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = productsQueryDto.PageNumber,
            PageSize = productsQueryDto.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / productsQueryDto.PageSize)
        };

        return new ResultDto<PaginatedResponse<ProductDto>>
        {
            IsSuccess = true,
            Data = resultData
        };
    }

    public async Task<ResultDto<ProductDto>> GetByIdAsync(int id)
    {
        var product = await _mapper.ProjectTo<ProductDto>(_context.Products.AsNoTracking())
                                .FirstOrDefaultAsync(w => w.Id == id, CancellationToken.None);
        if (product is null)
            return new ResultDto<ProductDto>
            {
                IsSuccess = false,
                Errors = ["Product not found"]
            };

        var dto = _mapper.Map<ProductDto>(product);
        return new ResultDto<ProductDto>
        {
            IsSuccess = true,
            Data = dto
        };
    }

    public async Task<ResultDto<ProductDto>> CreateAsync(CreateProductDto createProductDto)
    {
        var validationResult = new CreateProductValidator().Validate(createProductDto);
        if (!validationResult.IsValid)
            return new ResultDto<ProductDto>
            {
                IsSuccess = false,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };

        var product = _mapper.Map<Product>(createProductDto);
        product.CreateDate = DateTime.UtcNow;
        await _context.Products.AddAsync(product, CancellationToken.None);
        await _context.SaveChangesAsync(CancellationToken.None);
        var dto = _mapper.Map<ProductDto>(product);
        return new ResultDto<ProductDto>
        {
            IsSuccess = true,
            Data = dto
        };
    }

    public async Task<ResultDto<ProductDto>> UpdateAsync(int id, UpdateProductDto updateProductDto)
    {
        var validationResult = new UpdateProductValidator().Validate(updateProductDto);
        if (!validationResult.IsValid)
            return new ResultDto<ProductDto>
            {
                IsSuccess = false,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };
        var product = await _context.Products.FindAsync(id, CancellationToken.None);
        if (product is null)
            return new ResultDto<ProductDto>
            {
                IsSuccess = false,
                Errors = ["Product not found"]
            };
        _mapper.Map(updateProductDto, product);
        product.UpdateDate = DateTime.UtcNow;
        _context.Products.Update(product);
        await _context.SaveChangesAsync(CancellationToken.None);
        var dto = _mapper.Map<ProductDto>(product);
        return new ResultDto<ProductDto>
        {
            IsSuccess = true,
            Data = dto
        };
    }

    public async Task<ResultDto<bool>> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id, CancellationToken.None);
        if (product is null)
            return new ResultDto<bool>
            {
                IsSuccess = false,
                Errors = ["Product not found"]
            };
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(CancellationToken.None);
        return new ResultDto<bool>
        {
            IsSuccess = true,
            Data = true
        };

    }
}
