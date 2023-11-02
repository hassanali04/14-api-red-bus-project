namespace Assignment2.Models
{
    public class buh
    {
        public int Id { get; set; }
        public string Busnumber { get; set; }
    //    public string Route { get; set; }
        public int? DriverId { get; set; } // Nullable to 
        public Driver? Driver { get; set; }
    }
}
