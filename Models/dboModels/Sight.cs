namespace Attractions.Models.dboModels
{
    public class Sight
    {
        public int Id { get; set; }
        public string SightName { get; set; } = null!;
        public double AvgBall { get; set; }
        public int Id_City { get; set; }
    }
}
