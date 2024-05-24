using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddHotelController : ControllerBase
    {
        private readonly IAddHotel _repo;
        public AddHotelController(IAddHotel repo)
        {
            _repo = repo;
        }

        [HttpPost("AddHotel")]
        public async Task<ActionResult> AddHotel(HotelModel hotel)
        {
            try
            {
                var result = await _repo.AddHotels(hotel);

                if (result == null)
                {
                    return BadRequest("Failed to add hotel. Please check your data.");
                }

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("ViewHotel")]
        public async Task<ActionResult> GetAllHotels()
        {
            try
            {
                List<HotelModel> hotelmodel = await _repo.GethotelDetials();
                return Ok(hotelmodel);
            }
            catch
            {
                return NotFound("Thers is no approved datas is Found! please register and then check it.");
            }
        }

        [HttpDelete("DeleteHotelDetails")]
        public async Task<ActionResult> DeleteHotelDetails(int id, string role)
        {
            try
            {
                if (id == null)
                    return NotFound(); 

                var result = await _repo.DeleteHotelDetails(id, role);

                if (result == "Hotel Deleting Successfully")
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

        [HttpPut("HotelUpdate")]
        public async Task<ActionResult> AlterHotel([FromBody] HotelModel hotelModel)
        {
            var updaethotel = await _repo.UpdateHotel(hotelModel);
            return Ok(updaethotel);
        }

    }
}
