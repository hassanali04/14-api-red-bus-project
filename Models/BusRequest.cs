using System.ComponentModel.DataAnnotations;

namespace RedBus_Service.Request
{
    public class BusRequest
    {
        public int Id { get; set; }
        [Required] public string? Busnumber { get; set; }
  //    public Driver AssociatedDriver { get; set; }
  //      public int? DriverId { get; set; } // Nullable int to represent the driver associated with the bus
    }
}
