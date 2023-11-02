

namespace RedBus_Service.Request
{
    public class BusResponse
    {
        public int Id { get; set; }
        public string? Busnumber { get; set; }
  //      public string? Route { get; set; }
   //     public int? DriverId { get; set; } // Nullable int to represent the driver associated with the bus
        public List<DriverResponse> driverResponses { get; set; }
       
        // Navigation property to represent the associated driver
     //  public Driver AssociatedDriver { get; set; }
    }
}
