using Api_Web_React.Api.Data;
using Api_Web_React.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Web_React.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet("customerslist")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _dataContext.Customers.FromSqlRaw("EXEC GetCustomers").ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getcustomerbyid/{id:int}")]
        public async Task<IActionResult> GetCustomersById(int id)
        {
            try
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(customer);
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }

        [HttpPost("createcustomer")]
        public async Task<IActionResult> CreateAsync(Customer customer)
        {
            try
            {
                _dataContext.Customers.Add(customer);
                await _dataContext.SaveChangesAsync();
                return Ok(customer);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updatecustomer")]
        public async Task<IActionResult> UpdateAsync(Customer customer)
        {
            try
            {
                _dataContext.Customers.Update(customer);
                await _dataContext.SaveChangesAsync();
                return Ok(customer);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletecustomer/{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var customer = await _dataContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                _dataContext.Customers.Remove(customer);
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
        }
    }
}
