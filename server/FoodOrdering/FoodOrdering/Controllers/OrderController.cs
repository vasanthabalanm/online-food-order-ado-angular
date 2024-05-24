using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _repo;
        public OrderController(IOrder repo)
        {
            _repo = repo;
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult> AddOrders(OrdersModel orders)
        {
            try
            {
                var result = await _repo.AddOrders(orders);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
