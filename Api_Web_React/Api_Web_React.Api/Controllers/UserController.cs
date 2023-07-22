using Api_Web_React.Api.Data;
using Api_Web_React.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Api_Web_React.Api.Controllers
{
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet("userlist")]
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

        [HttpGet("getuserbyid/{id:int}")]
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

        [HttpPost("createuser")]
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

        [HttpPut("updateuser")]
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

        [HttpDelete("deleteuser/{id:int}")]
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
