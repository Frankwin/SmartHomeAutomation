using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManufacturersController : Controller
    {
        private readonly IManufacturerService manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
        }
        
        /// <summary>
        /// Get all the manufacturers
        /// </summary>
        /// <returns>List of Manufacturers in JSON format</returns>
        // GET: api/<controller>
        [HttpGet]
        public IActionResult GetAllManufacturers()
        {
            return Ok(manufacturerService.GetAll().ToList());
        }

//        /// <summary>
//        /// Get a manufacturer
//        /// </summary>
//        /// <param name="id">Manufacturer GUID</param>
//        /// <returns>Manufacturer object in JSON format</returns>
//        // GET api/<controller>/<guid>
//        [HttpGet("{id}", Name="GetManufacturer")]
//        public async Task<IActionResult> GetManufacturer([FromRoute] Guid id)
//        {
//            var manufacturer = await context.Manufacturers.FirstOrDefaultAsync(m => m.ManufacturerId == id && !m.IsDeleted);
//            if (manufacturer == null)
//            {
//                return NotFound();
//            }
//            return Ok(manufacturer);
//        }
//
//        /// <summary>
//        /// Create a new manufacturer
//        /// </summary>
//        /// <param name="manufacturer">Manufacturer object from body</param>
//        /// <returns>Newly created manufacturer object in JSON format</returns>
//        // POST api/<controller>
//        [HttpPost]
//        public async Task<IActionResult> PostManufacturer([FromBody]Manufacturer manufacturer)
//        {
//            if (manufacturer == null)
//            {
//                return BadRequest();
//            }
//            context.Manufacturers.Add(manufacturer);
//            await context.SaveChangesAsync();
//
//            return CreatedAtRoute("GetManufacturer", new { id = manufacturer.ManufacturerId }, manufacturer);
//        }
//
//        /// <summary>
//        /// Update a manufacturer
//        /// </summary>
//        /// <param name="id">Manufacturer GUID from URL</param>
//        /// <param name="manufacturer">Manufacturer object from body</param>
//        /// <returns>Updated manufacturer object in JSON format</returns>
//        // PUT api/<controller>/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutManufacturer([FromRoute]Guid id, [FromBody]Manufacturer manufacturer)
//        {
//            if (id != manufacturer.ManufacturerId)
//            {
//                return BadRequest("ID does not match");
//            }
//
//            context.Entry(manufacturer).State = EntityState.Modified;
//
//            try
//            {
//                await context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ManufacturerExists(id))
//                {
//                    return NotFound();
//                }
//            }
//            return Ok(manufacturer);
//        }
//
//        /// <summary>
//        /// Delete a manufacturer
//        /// </summary>
//        /// <param name="id">Manufacturer GUID from URL</param>
//        /// <returns>Deleted manufacturer object in JSON format</returns>
//        // DELETE api/<controller>/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteManufacturer([FromRoute]Guid id)
//        {
//            var manufacturer = await context.Manufacturers.SingleOrDefaultAsync(m => m.ManufacturerId == id);
//            if (manufacturer == null)
//            {
//                return NotFound();
//            }
//
//            manufacturer.IsDeleted = true;
//            await context.SaveChangesAsync();
//
//            return Ok(manufacturer);
//        }
//
////        [HttpGet("{id}/devices")]
////        public async Task<IActionResult> GetDevicesForManufacturer([FromRoute] Guid id)
////        {
////            return null;
////        }
//
//        private bool ManufacturerExists(Guid id)
//        {
//            return context.Manufacturers.Any(e => e.ManufacturerId == id);
//        }
    }
}
