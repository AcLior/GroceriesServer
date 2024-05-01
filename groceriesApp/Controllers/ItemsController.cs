using groceriesApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace groceriesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        [HttpPost]
      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public IActionResult AddProduct(string email, string productName,string listName)
        {
            try
            {

                Item item = new Item();
                item.AddProduct(email,productName,listName);

                // Return success response
                return Ok("Item added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{email}/{listName}/{productName}")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteProduct(string email,string listName,string productName)
        {
            try
            {

                Item item = new Item();
                // Call the AddUser method in UsersDB to add the user
                item.DeleteProduct(email,listName,productName);

                // Return success response
                return Ok("User deleted successfully.");

            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("{email}/{categoryName}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult AddCategory(string email, string categoryName)
        {
            try
            {


                Item item = new Item();
                item.AddCategory(email, categoryName);

                // Return success response
                return Ok("Item added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
