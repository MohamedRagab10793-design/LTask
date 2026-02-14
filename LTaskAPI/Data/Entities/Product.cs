namespace LTaskAPI.Data.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public decimal Price { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }

}
