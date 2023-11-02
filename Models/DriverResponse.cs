using RedBus_Service.Response;
using System.ComponentModel.DataAnnotations;

namespace RedBus_Service.Request
{
    public class DriverResponse
    {
        public BusResponse BusResponse { get; set; }
        public int Id {  get; set; }
        public string Name {  get; set; }
        public int Age { get; set; }
         
    //    public Buses buses { get; set; }
    //    public List<BusResponse> busResponses { get; set; }
     //   public Bus AssociatedBus { get; set; }

    }
}