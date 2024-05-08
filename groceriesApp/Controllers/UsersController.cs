using groceriesApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace groceriesApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
       

        [HttpPost]
        [Route("CheckAccount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(string email,string password)
        {
            try
            {
                
                User user = new User();
               
                // Return success response
                return Ok(user.CheckUser(email, password));
              
               
            }
            catch (Exception e)
            {
                return StatusCode(500, $"An error occurred: {e.Message}");
    }
        }


        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PostReg(string email,string password)
        {
            try
            {
                User user = new User();
                // Call the AddUser method in UsersDB to add the user     

                // Return success response
                return Ok(user.AddUser(email,password));

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
        
        public IActionResult Delete(string email)
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
