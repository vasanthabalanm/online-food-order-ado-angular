using FoodOrderBAL.Interfaces;
using FoodOrderBAL.Services;
using FoodOrderDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IAddMenu _addmenu;
        public MenuController(IAddMenu addmenu)
        {
            _addmenu = addmenu;
        }

        [HttpPost("Addmenu")]
        public async Task<ActionResult> AddMenuItems(MenuDetailsModel menumodel)
        {
            try
            {
                var result = await _addmenu.AddMenu(menumodel);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("ViewMenu")]
        public async Task<ActionResult> GetMenuItems()
        {
            List<MenuDetailsModel> menuItems = await _addmenu.GetMenuDetials();
            return Ok(menuItems);
        }

        [HttpPut("updateMenu")]
        public async Task<ActionResult> UpdateMenuDetails([FromBody]MenuDetailsModel menu)
        {
            var results =  await _addmenu.UpdateMenu(menu);
            return Ok(results);
        }

        [HttpDelete("DeleteMenu")]
        public async Task<ActionResult> DeleteMenu(int id, int hotelbranchid)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var result = await _addmenu.DeleteMenuDetails(id, hotelbranchid);

                if (result == "MenuItem Deleted Successfully")
                {
                    return Ok(new
                    {
                        Status = 200,
                        Message = "MenuItem has been deleted"
                    });
                }
                else
                {
                    return BadRequest("Menu Item deletion failed. Please check your request parameters.");
                }
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
