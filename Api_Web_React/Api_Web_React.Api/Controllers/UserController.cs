using Api_Web_React.Api.Data;
using Api_Web_React.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_Web_React.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _dataContext.Users.FromSqlRaw("EXEC GetUsers").ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(User user)
        {
            try
            {
                _dataContext.Users.Add(user);
                await _dataContext.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(User user)
        {
            try
            {
                _dataContext.Users.Update(user);
                await _dataContext.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                _dataContext.Users.Remove(user);
                await _dataContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound();
            }
        }
    }
}
