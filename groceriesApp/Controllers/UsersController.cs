using groceriesApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace groceriesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       

        [HttpGet("{email}")]
     
        public  int GetID(string email)
        {
            try
            {
                User u = new User();

                // Return success response
                return u.GetID(email); ;
              
               
            }
            catch (Exception e)
            {
                return -1;
            }
        }



        [HttpPost]

        public IActionResult AddUser([FromBody] User user)
        {
            try
            {
                User u = new User();
                // Call the AddUser method in UsersDB to add the user
                u.AddUser(user);

                // Return success response
                return Ok("User added successfully.");
            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
     
        [HttpDelete("{email}")]
     
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public IActionResult DeleteUser(string email)
        {
            try
            {

                User u = new User();
                // Call the AddUser method in UsersDB to add the user
                u.DeleteUser(email);

                // Return success response
                return Ok("User deleted successfully.");

            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
