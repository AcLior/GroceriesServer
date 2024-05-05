using groceriesApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace groceriesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroceryListController : ControllerBase
    {


        [HttpPost]
      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
      
        public IActionResult CreateList(string nameList,string email)
        {
            try
            {
               
                GroceryList gl = new GroceryList();
                gl.CreateList(nameList,email);

                // Return success response
                return Ok("list created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{email},{listName}")]

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult DeleteList(string email, string listName)
        {
            try
            {

                GroceryList gl = new GroceryList();
          
                gl.DeleteList(email, listName);

                // Return success response
                return Ok("List deleted successfully.");

            }
            catch (Exception ex)
            {
                // Return error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/<GroceryListController>
        [HttpGet]
        [Route("GetID/{email}/{listName}")]
        public int GetID(string email,string listName)
        {

            return GroceryList.GetListID(email,listName);
        }

        // GET: api/<GroceryListController>
        //[HttpGet]
        //[Route("GetID/{email}")]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<GroceryList[]> GetListByEmail(string email)
        //{
        //    try
        //    {
        //        GroceryList.GetListByEmail(email);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        //    }
        //}

    }
}
