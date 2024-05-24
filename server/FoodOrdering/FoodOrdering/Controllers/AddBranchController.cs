using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddBranchController : ControllerBase
    {
        private readonly IAddHotelBranch _addhotel;
        public AddBranchController(IAddHotelBranch addhotel)
        {
            _addhotel = addhotel;
        }

        [HttpPost("AddBranch")]
        public async Task<ActionResult> AddBranch( HotelBranchModel hotelmodel)
        {
            try
            {

                var result = await _addhotel.AddBranch(hotelmodel);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdateBranch")]
        public async Task<ActionResult> UpdateHotelDetails([FromBody] HotelBranchModel hotel)
        {
            try
            {
                var updateHotel = await _addhotel.UpdateHotel(hotel);
                return Ok(updateHotel); 
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Could not update {ex.Message}");
            }
            
        }

        [HttpGet("DisplayBranch")]
        public async Task<ActionResult> GetHotelBranch()
        {
            try
            {
                List<HotelBranchModel> hotelbranch = await _addhotel.GethotelDetials();
                if (hotelbranch == null || hotelbranch.Count == 0)
                {
                    return BadRequest("There is no data");
                }
                return Ok(hotelbranch);

            }
            catch (InvalidOperationException ex)
    {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("DeleteBranch")]
        public async Task<ActionResult> DeleteHotelBranch(int id, int hotelid)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var result = await _addhotel.DeleteHotelbranch(id, hotelid);

                if (result == "Hotel branch Deleting Successfully")
                {
                    return Ok(new
                    {
                        Status = 200,
                        Message = "Hotel has been deleted"
                    });
                }
                else
                {
                    return BadRequest("Hotel deletion failed. Please check your request parameters.");
                }
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
