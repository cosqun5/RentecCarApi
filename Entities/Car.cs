namespace RenteCarApi.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelYear { get; set; }
        public double DailyPrice { get; set; }
        public string Description { get; set; }
        
        public int BrandId { get; set; }

        public int ColorId { get; set; }
    }
}
