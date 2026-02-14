namespace LTaskAPI.DTOs;

public class GetAllProductsQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string SearchText { get; set; }
}
