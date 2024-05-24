using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovedUserController : ControllerBase
    {
        private readonly IApprovedUser _approvedUser;

        #region Constructor
        public ApprovedUserController(IApprovedUser approvedUser)
        {
            _approvedUser = approvedUser;
        }
        #endregion

        

        #region  Add user
        /// <summary>
        /// Get details from the logged User
        /// </summary>
        /// <param name="user"></param>
        /// <returns>list of Approved users</returns>

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> AddApprovedUser(ApprovedUsersModel usersModel)
        {
            var result = await _approvedUser.AddApprovedUsers(usersModel);
            return Ok(result);
        }
        #endregion

        #region Get allApproved users
        /// <summary>
        /// Get all approved users List
        /// </summary>
        /// <returns>List of approved users</returns>

        [HttpGet("AllApprovedUsers")]
        public async Task<ActionResult> GetAllApprovedUsers()
        {
            List<ApprovedUsersModel> usersModels = await _approvedUser.GetAllUserDetails();
            return Ok(usersModels);
        }
        #endregion

       /* #region Expecting the Autentication credentials
        /// <summary>
        /// Post Login Details 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return the Autentication credentials</returns>
        [HttpPost("login")]
        public async Task<ActionResult> LoginAdminDetails(ApprovedUsersModel user)
        {
            try
            {
                var getuser = await _approvedUser.GetUserInfoByEmail(user);
                if (getuser == null)
                {
                    return NotFound("User is not found");
                }
                return Ok(new LoginDTO()
                {
                    Id = getuser.Id,
                    Email = getuser.Email,
                    Role = getuser.UserRole,
                    OldPassword = getuser.TempPassword
                });
            }
            catch (Exception ex)
            {
                if (ex.Message == "password is incorrect")
                {
                    return BadRequest(ex.Message);
                }
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Get inly User Role details
        /// <summary>
        /// get only Approved user Role Lists
        /// </summary>
        /// <returns>List of user Role List</returns>
        [HttpGet("OnlyApprovedUsers")]
        public async Task<ActionResult> GetOnlyUsersDetails()
        {
            try
            {
                List<ApprovedUsersModel> usersModels = await _approvedUser.GetOnlyUserDetails();
                return Ok(usersModels);
            }
            catch
            {
                return NotFound("Thers is no approved datas is Found! please register and then check it.");
            }

        }
        #endregion

        #region Get the Vendor Role List 
        /// <summary>
        /// Get the vendor role list
        /// </summary>
        /// <returns>List the vendor role lists</returns>
        [HttpGet("OnlyApprovedVendors")]
        public async Task<ActionResult> GetOnlyVendorDetails()
        {
            try
            {
                List<ApprovedUsersModel> usersModels = await _approvedUser.GetOnlyVendorDetails();
                return Ok(usersModels);
            }
            catch
            {
                return NotFound("Thers is no approved datas is Found! please register and then check it.");
            }
        }
        #endregion

        #region Update the Approved User Profile
        /// <summary>
        /// Update the Approved User Profile 
        /// </summary>
        /// <param name="approvedUsersModel"></param>
        /// <returns>the response what you done changes</returns>
        [HttpPut("ApprovedUserUpdate")]
        public async Task<ActionResult> UpdateUserDetails ([FromBody] ApprovedUsersModel approvedUsersModel)
        {
            var updaetUser = await _approvedUser.UpdateUserDetails(approvedUsersModel);
            return Ok(updaetUser);
        }
        #endregion

        #region Delete the Approved User
        /// <summary>
        /// Delete the Approved Users
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns>Only string as deleted Successfully</returns>
        [HttpDelete("DeleteUserDetails")]
        public async Task<ActionResult> DeleteUsersDetails(int id,string email)
        {
            try
            {
                if (email == null)
                    return NotFound();
                var result = await _approvedUser.DeleteUserDetails(id,email);
                return Ok(new
                {
                    Status = 200,
                    Message = "ApprovedUser has been deleted"
                });
            }
            catch
            {
                return NotFound("There is no user");
            }
        }
        #endregion

        #region Get the counts of toatal hotel
        /// <summary>
        /// Get the counts of the total hotel
        /// </summary>
        /// <returns>only counts asa int type</returns>
        [HttpGet("TotalHotelCounts")]
        public async Task<ActionResult<int>> GetTotalHotel()
        {
            try
            {
                int totalApprovedHotels = await _approvedUser.TotalapprovedHotel();
                var result = new { TotalHotel = totalApprovedHotels };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region Get the Counts of Total Branch
        /// <summary>
        /// Get the Counts of Total Branch
        /// </summary>
        /// <returns>Only return Int</returns>
        [HttpGet("TotalBranchCounts")]
        public async Task<ActionResult<int>> GetAllBranch()
        {
            try
            {
                int totalApprovedBranch = await _approvedUser.TotalapprovedBranchl();
                var result = new { TotalBranch = totalApprovedBranch };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region Get the Counts of Total Approved User Role
        /// <summary>
        ///  Get the Counts of Total Approved User Role
        /// </summary>
        /// <returns>return int value</returns>
        [HttpGet("ApprovedUserCounts")]
        public async Task<ActionResult<int>> GetApproveUser()
        {
            try
            {
                int totalApproveduser = await _approvedUser.TotalapprovedUser();
                var result = new { ApprovedUser = totalApproveduser };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region  Get the Counts of Total Approved vendor Role
        /// <summary>
        ///  Get the Counts of Total Approved vendor Role
        /// </summary>
        /// <returns>return int type</returns>
        [HttpGet("approvedVendorCounts")]
        public async Task<ActionResult<int>> GetApproveVendor()
        {
            try
            {
                int totalApprovedVendor = await _approvedUser.TotalapprovedVendor();
                var result = new { ApprovedVendor = totalApprovedVendor  };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region  Get the Counts of Total Pending User Role
        /// <summary>
        ///  Get the Counts of Total Pending User Role
        /// </summary>
        /// <returns>return int type</returns>
        [HttpGet("PendingUserCounts")]
        public async Task<ActionResult<int>> GetpendingUser()
        {
            try
            {
                int totalPendingUser = await _approvedUser.TotalpendingUser();
                var result = new { PendingUSer =  totalPendingUser};
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region  Get the Counts of Total Pending Vendor Role
        /// <summary>
        ///  Get the Counts of Total Pending Vendor Role
        /// </summary>
        /// <returns>return int type</returns>
        [HttpGet("VendorPendingCounts")]
        public async Task<ActionResult<int>> GetpendingVendor()
        {
            try
            {
                int totalPendingvendor = await _approvedUser.Totalpendingvendor();
                var result = new { pendingvendor = totalPendingvendor };
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion

        #region  Get the Counts of New Arrivals
        /// <summary>
        /// Get the count of new arrivals
        /// </summary>
        /// <returns>return int type</returns>
        [HttpGet("NewArrivals")]
        public async Task<ActionResult> GetNewArrivals()
        {
            try
            {
                var getUsers = await _approvedUser.GetNewArrivals();

                if (getUsers == null || getUsers.Count == 0)
                {
                    return NotFound("There is no data found");
                }

                List<NewArrivalsDto> newArrivalsDtoList = new List<NewArrivalsDto>();

                foreach (var user in getUsers)
                {
                    newArrivalsDtoList.Add(new NewArrivalsDto()
                    {
                        UserName = user.UserName,
                        UserRole = user.UserRole,
                        Email = user.Email
                    });
                }

                return Ok(newArrivalsDtoList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update the Approved user Password for one time
        /// <summary>
        /// Update the Approved user Password for one time
        /// </summary>
        /// <param name="approvedUsersModel"></param>
        /// <returns>return int Type </returns>
        [HttpPut("UpdatePassword")]
        public async Task<ActionResult> UpdatePassowrd([FromBody] ApprovedUsersModel approvedUsersModel)
        {
            var updatePass = await _approvedUser.UpdatePassword(approvedUsersModel);
            return Ok(updatePass);
        }
        #endregion

        #region Update the Order change as Approved once trigger email to the end user
        /// <summary>
        /// Update the Order change as Approved once trigger email to the end user
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="role"></param>
        /// <returns>return the order status as approved</returns>
        [HttpPut("OrderStatusChange")]
        public async Task<ActionResult> OrderStatusChange(int orderid,string role)
        {
            try
            {
                var result = await _approvedUser.StatusApproved(orderid, role);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region To View the all pending Order to change approve
        /// <summary>
        /// To View the all pending Order to change approve
        /// </summary>
        /// <returns>return the List of all order details</returns>
        [HttpGet("ViewOrders")]
        public async Task<ActionResult> GetOrderDetails()
        {
            try
            {
                var result = await _approvedUser.GetOrderDetails();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
        #endregion*/
    }
}
