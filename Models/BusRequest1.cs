using System.ComponentModel.DataAnnotations;

namespace RedBus_Service.Request
{
    public class BusRequest1
    {
      //  public int Id { get; set; }
        [Required] public string? Busnumber { get; set; }
    }
}
