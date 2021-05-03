using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CustomerReadModel>> Get()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerReadModel> Get(int id)
        {
            if (id<=0)
            {
                return BadRequest("Invalid id value.");
            }
            var customer = _service.Get(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(customer);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CustomerUpdateModel customer)
        {
            var id = _service.Create(customer);
            var uri = $"/api/customer/{id}";
            return Created(uri, id);
        }

        [HttpPut("{id}")]
        public ActionResult<string> Put(int id, [FromBody] CustomerUpdateModel customer)
        {
            if (id <= 0 || !_service.Exists(id))
            {
                return NotFound();
            }

            var error = _service.ValidateUpdate(customer);
            if (!string.IsNullOrWhiteSpace(error))
            {
                return BadRequest(error);
            }
            else
            {
                _service.Update(id, customer);
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            if (id <= 0 || !_service.Exists(id))
            {
                return NotFound();
            }
            _service.Delete(id);
            return Ok();
        }
    }
}
