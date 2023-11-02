using System.ComponentModel.DataAnnotations;

namespace RedBus_Service.Request
{
    public class DriverRequest1
    {
        [Required] public string? Name { get; set; }
        [Range(18, 55, ErrorMessage = "Age must be between 18 and 55.")]
        public int Age { get; set; }
    }
}
