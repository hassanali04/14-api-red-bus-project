using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace RedBus_Service.Request
{
    public class DriverRequest
    {
        [Required] public int Id;
        [Required] public string? Name;
        [Range(18, 55, ErrorMessage = "Age must be between 18 and 55.")]
        public int Age { get; set; }
      //  public bus AssociatedBus { get; set; }
    }
}
