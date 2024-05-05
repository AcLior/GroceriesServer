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
       
        public IActionResult Post(Item item,string email)
        {
            try
            {
                // Return success response
                return Ok(Item.AddProduct(item, email));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{item}/{email}")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(Item item, string email)
        {
            try
            {
             
                // Return success response
                return Ok(Item.DeleteProduct(item.ProductName, email));


            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{item}/{email}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Put(Item item, string email)
        {

            try
            {

                // Return success response
                return Ok(Item.UpdateProduct(item.ProductName, email));


            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
           
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Item> Get(string email)
        {
            try
            {
                return Ok(Item.GetListByEmail(email));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status400BadRequest, e.Message);
            }
        }


    }
}
