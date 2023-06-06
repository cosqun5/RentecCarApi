namespace RenteCarApi.Entities.Dtos.Car;

public class CarCreateDto
{
    public string Name { get; set; }
    public int ModelYear { get; set; }
    public double DailyPrice { get; set; }
    public string Description { get; set; }
    public int BrandId { get; set; }
    public int ColorId { get; set; }
}
