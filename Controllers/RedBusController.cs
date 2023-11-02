using Assignment2.data;
using Assignment2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedBus_Service.Request;
using RedBus_Service.Response;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Assignment2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedBusController : ControllerBase
    {
        public DataContext _context;
        public RedBusController(DataContext context)
        {
            _context = context;
        }
        //1
        [HttpPost("BUS")]
        public ActionResult<BusResponse1> PostDetail(BusRequest1 request)
        {
            var bus = new buh
            {
                Busnumber = request.Busnumber,
                //   Route = request.Route,
            };
            _context.buses.Add(bus);
            _context.SaveChanges();

            var result = new BusResponse1
            {
                Busnumber = request.Busnumber,
                //  Route = request.Route,                
            };
            return result;
        }
        //2
        [HttpPut("BUSES")]
        public ActionResult<bool> Update(int id, BusRequest1 request)
        {
            var b = _context.buses
                .FirstOrDefault(m => m.Id == id);
            if (b == null)
                return NotFound("Brand Not Found.");
            //  b.Route = request.Route;
            b.Busnumber = request.Busnumber;
            _context.Update(b);
            _context.SaveChanges();
            return true;
        }
        //3
        [HttpDelete("{id}")]
        public async Task<ActionResult<BusResponse1>> deletethebus(int id)
        {
            var b = await _context.buses.FindAsync(id);
            if (b == null) return NotFound();
            { return NotFound(); }
            _context.buses.Remove(b);
            await _context.SaveChangesAsync(); return Ok();
        }
        //4
        [HttpGet("Listofallbuses")]
        public ActionResult<IEnumerable<BusResponse1>> GetResponse(string searchkeyword,int pagenumber=1,int count =10)
        {
            var query = _context.buses.AsQueryable();
            if (!string.IsNullOrEmpty(searchkeyword))
                query = query.Where(buh => buh.Busnumber.Contains(searchkeyword));
            var bu = query
                .Select(b => new BusResponse1
                {
                    Id = b.Id,
                    Busnumber = b.Busnumber,
                })
                .Skip((pagenumber - 1) * count)
                .Take(count)
                .ToList();
            return bu;
        }
        //5
        [HttpPost("Driver")]
        public ActionResult<DriverResponse1> PostDDetail(DriverRequest1 request)
        {
            var driv = new Driver
            {
                Name = request.Name,
                Age = request.Age,
                //   Route = request.Route,
            };
            _context.drivers.Add(driv);
            _context.SaveChanges();

            var result = new DriverResponse1
            {
                Age = request.Age,
                Name = request.Name,

                //  Route = request.Route,                
            };
            return result;
        }
        //6
        [HttpPut("Driver")]
        public ActionResult<bool> Update(int id, DriverRequest1 request)
        {
            var b = _context.drivers
                .FirstOrDefault(m => m.Id == id);
            if (b == null)
                return NotFound("Driver Not Found.");
            b.Name = request.Name;
            b.Age = request.Age;
            _context.Update(b);
            _context.SaveChanges();
            return true;
        }
        //7
        [HttpDelete("Driver/{id}")]
        public async Task<ActionResult<bool>> deletethedriver(int id)
        {
            var c = await _context.drivers.FindAsync(id);
            if (c == null) return NotFound();
            { return NotFound(); }
            _context.drivers.Remove(c);
            await _context.SaveChangesAsync(); return Ok();
        }
        //8
        [HttpGet("ListAllDrivers")]
        public List<DriverResponse1> ListAllDrivers()
        {
            return _context.drivers
                .Select(d => new DriverResponse1
                {
                    Id = d.Id,
                    Name = d.Name,
                    Age = d.Age,
                })
                .ToList();
        }
        ///9
        [HttpGet("BusesAssociatedWithDriver/{driverId}")]
        public ActionResult<DriverWithResponse> BusesAssociatedWithDriver(int driverId)
        {
            var driver = _context.drivers.FirstOrDefault(d => d.Id == driverId);
            if (driver == null)
                return NotFound("Driver Not Found.");

            var buses = _context.buses.Where(b => b.DriverId == driverId).ToList();
            var response = new DriverWithResponse
            {
                Driver = driver,

            };


            return Ok(buses);
        }
        // 10
        // List of drivers associated with a bus
        [HttpGet("DriversAssociatedWithBus/{busNumber}")]
        public ActionResult<DriverResponse> DriversAssociatedWithBus(string busNumber)
        {
            var bus = _context.buses.FirstOrDefault(b => b.Busnumber == busNumber);
            if (bus == null)
                return NotFound("Bus Not Found.");

            var driver = _context.drivers.FirstOrDefault(d => d.Id == bus.DriverId);

            if (driver == null)
                return Ok("No driver associated with this bus.");

            return Ok(driver);
        }
        private readonly List<buh> buses = new List<buh>();
        private readonly List<Driver> drivers = new List<Driver>();
        // 11. List of Unassociated Drivers
        [HttpGet("drivers/unassociated")]
        public IActionResult UnassociatedDrivers()
        {
            var allDrivers = _context.drivers.ToList(); // Retrieve all drivers from the database
            var unassociatedDrivers = allDrivers.Where(d => !_context.buses.Any(b => b.DriverId == d.Id)).ToList();
            return Ok(unassociatedDrivers);
        
    }

        // 12. List of Unassociated Buses
        [HttpGet("buses/unassociated")]
        public ActionResult<BusResponse1> UnassociatedBuses()
        {
            var unassociatedBuses = _context.buses.Where(b => b.DriverId == null).ToList();
            return Ok(unassociatedBuses);
        }

        // 13. Associate a Driver with a Bus
        [HttpPost("drivers/{driverId}/buses/{busId}")]
        public ActionResult<DriverRequest1> AssociateDriverWithBus(int driverId, int busId)
        {
            var driver = _context.drivers.FirstOrDefault(d => d.Id == driverId);
            var bus = _context.buses.FirstOrDefault(b => b.Id == busId);

            if (driver == null || bus == null)
                return NotFound("Driver or Bus Not Found.");

            bus.DriverId = driver.Id;
            _context.SaveChanges();
            return Ok($"Driver with ID {driverId} is now associated with Bus ID {busId}.");
        }

        // 14. Unassociate a Driver from a Bus
        [HttpDelete("drivers/{driverId}/buses/{busId}")]
        public IActionResult UnassociateDriverFromBus(int driverId, int busId)
        {
            var bus = buses.FirstOrDefault(b => b.Id == busId);

            if (bus == null)
                return NotFound("Bus Not Found.");

            if (bus.DriverId == driverId)
            {
                bus.DriverId = null;
                return Ok($"Driver with ID {driverId} is now unassociated from Bus ID {busId}.");
            }
            else
            {
                return BadRequest("Driver is not associated with the specified bus.");
            }
          
        }

    }


}






/*[Route("api/[controller]")]
[ApiController]
public class RedBusController : ControllerBase
{
    private readonly List<Bus> buses = new List<Bus>();
    private readonly List<Driver> drivers = new List<Driver>();

    // 1. Creating a Bus
    [HttpPost("buses")]
    public IActionResult CreateBus(Bus bus)
    {
        buses.Add(bus);
        return Ok(bus);
    }

    // 2. Updating a Bus
    [HttpPut("buses/{id}")]
    public IActionResult UpdateBus(int id, Bus updatedBus)
    {
        var bus = buses.FirstOrDefault(b => b.Id == id);
        if (bus == null)
            return NotFound();

        bus.BusNumber = updatedBus.BusNumber;
        bus.Route = updatedBus.Route;

        return Ok(true);
    }

    // 3. Deleting a Bus
    [HttpDelete("buses/{id}")]
    public IActionResult DeleteBus(int id)
    {
        var bus = buses.FirstOrDefault(b => b.Id == id);
        if (bus == null)
            return NotFound();

        buses.Remove(bus);

        return NoContent();
    }

    // 4. List of Buses in the System
    [HttpGet("buses")]
    public IActionResult ListBuses()
    {
        return Ok(buses);
    }

    // 5. Creating a Driver
    [HttpPost("drivers")]
    public IActionResult CreateDriver(Driver driver)
    {
        drivers.Add(driver);
        return Ok(driver);
    }

    // 6. Updating a Driver
    [HttpPut("drivers/{id}")]
    public IActionResult UpdateDriver(int id, Driver updatedDriver)
    {
        var driver = drivers.FirstOrDefault(d => d.Id == id);
        if (driver == null)
            return NotFound();

        driver.Name = updatedDriver.Name;
        driver.Age = updatedDriver.Age;

        return Ok(true);
    }

    // 7. Deleting a Driver
    [HttpDelete("drivers/{id}")]
    public IActionResult DeleteDriver(int id)
    {
        var driver = drivers.FirstOrDefault(d => d.Id == id);
        if (driver == null)
            return NotFound();

        drivers.Remove(driver);

        return NoContent();
    }

    // 8. List of All Drivers in the System
    [HttpGet("drivers")]
    public IActionResult ListDrivers()
    {
        return Ok(drivers);
    }

    // 9. List of Buses Associated with Drivers
    [HttpGet("drivers/{driverId}/buses")]
    public IActionResult BusesAssociatedWithDriver(int driverId)
    {
        var driver = drivers.FirstOrDefault(d => d.Id == driverId);
        if (driver == null)
            return NotFound();

        var associatedBuses = buses.Where(b => b.DriverId == driverId).ToList();
        return Ok(associatedBuses);
    }

    // 10. List of Drivers Associated with a Bus
    [HttpGet("buses/{busId}/drivers")]
    public IActionResult DriversAssociatedWithBus(int busId)
    {
        var bus = buses.FirstOrDefault(b => b.Id == busId);
        if (bus == null)
            return NotFound();

        var driver = drivers.FirstOrDefault(d => d.Id == bus.DriverId);
        if (driver == null)
            return Ok("No driver associated with this bus.");

        return Ok(driver);
    }

    // 11. List of Unassociated Drivers
    [HttpGet("drivers/unassociated")]
    public IActionResult UnassociatedDrivers()
    {
        var unassociatedDrivers = drivers.Where(d => !buses.Any(b => b.DriverId == d.Id)).ToList();
        return Ok(unassociatedDrivers);
    }

    // 12. List of Unassociated Buses
    [HttpGet("buses/unassociated")]
    public IActionResult UnassociatedBuses()
    {
        var unassociatedBuses = buses.Where(b => b.DriverId == null).ToList();
        return Ok(unassociatedBuses);
    }

    // 13. Associate a Driver with a Bus
    [HttpPost("drivers/{driverId}/buses/{busId}")]
    public IActionResult AssociateDriverWithBus(int driverId, int busId)
    {
        var driver = drivers.FirstOrDefault(d => d.Id == driverId);
        var bus = buses.FirstOrDefault(b => b.Id == busId);

        if (driver == null || bus == null)
            return NotFound("Driver or Bus Not Found.");

        bus.DriverId = driver.Id;

        return Ok($"Driver with ID {driverId} is now associated with Bus ID {busId}.");
    }

    // 14. Unassociate a Driver from a Bus
    [HttpDelete("drivers/{driverId}/buses/{busId}")]
    public IActionResult UnassociateDriverFromBus(int driverId, int busId)
    {
        var bus = buses.FirstOrDefault(b => b.Id == busId);

        if (bus == null)
            return NotFound("Bus Not Found.");

        if (bus.DriverId == driverId)
        {
            bus.DriverId = null;
            return Ok($"Driver with ID {driverId} is now unassociated from Bus ID {busId}.");
        }
        else
        {
            return BadRequest("Driver is not associated with the specified bus.");
        }
    }
}
}


*/