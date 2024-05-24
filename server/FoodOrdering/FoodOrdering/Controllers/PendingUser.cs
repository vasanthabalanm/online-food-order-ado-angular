using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingUser : ControllerBase
    {
        private readonly IPendingUser _pendingUser;
        public PendingUser(IPendingUser pendingUser)
        {
            _pendingUser = pendingUser;
        }

        [HttpPost("PendingUser")]
        public async Task<ActionResult> AddPendingUser(PendingUserModel model)
        {
            try
            {
                var result = await _pendingUser.AddPendingUsers(model);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("DisplayPendingUser")]
        public async Task<ActionResult> GetPendingUsers()
        {
            List<PendingUserModel> pendingUser = await _pendingUser.GetOnlyPendingUserDetails();
            return Ok(pendingUser);
        }

        [HttpDelete("DeletePendingUser")]
        public async Task<ActionResult> DeletePendingUsersDetails(int id)
        {
            try
            {
                if (id == null)
                    return NotFound("Empty List");
                var result = await _pendingUser.DeletePendingUserDetails(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Pending User has been deleted"
                });
            }
            catch
            {
                return NotFound("There is no user");
            }
        }

        //vendor
        [HttpPost("PendingVendor")]
        public async Task<ActionResult> AddPendingvendor(PendingUserModel model)
        {
            try
            {
                var result = await _pendingUser.AddPendingVendor(model);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("DisplayPendingVendor")]
        public async Task<ActionResult> GetPendingVendor()
        {
            List<PendingUserModel> pendingVendor = await _pendingUser.GetOnlyPendingVendorDetails();
            return Ok(pendingVendor);
        }

        [HttpDelete("DeletePendingVendor")]
        public async Task<ActionResult> DeletePendingVendorDetails(int id)
        {
            try
            {
                if (id == null)
                    return NotFound("Empty List");
                var result = await _pendingUser.DeletePendingVendorDetails(id);
                return Ok(new
                {
                    Status = 200,
                    Message = "Pending Vendor has been deleted"
                });
            }
            catch
            {
                return NotFound("There is no Vendor");
            }
        }
    }
}
