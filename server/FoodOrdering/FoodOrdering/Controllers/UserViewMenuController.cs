using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models.DTO;
using FoodOrdering.ExceptionsHandler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserViewMenuController : ControllerBase
    {
        private readonly IUserViewMenu _repo;

        #region Constructor to intialize value
        public UserViewMenuController(IUserViewMenu repo)
        {
            _repo = repo;
        }
        #endregion

        #region To get Menu lists
        [HttpGet ("UserViewMenu")]
        public async Task<ActionResult> UserViewMenuDetails()
        {
            try
            {
                List<UserViewMenuDTO> menuList = await _repo.UserViewMenuList();
                return Ok(menuList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Filter By Location and Menuitem name
        [HttpGet("FilterbyLocation")]
        public async Task<ActionResult> FilterByLocation(string menuName, string branchLocation)
        {
            var menu = await _repo.GetMenuFromLocation(menuName, branchLocation);
            if(menu == null)
            {
                throw new NotFoundExceptionHandler("There is no data");
            }
            else
            {
                return Ok(menu);
            }
        }
        #endregion

        #region show pending order
        [HttpGet("UserViewPendingOrder")]
        public async Task<ActionResult> UserViewPendingOrderDetails(int id)
        {
            try
            {
                List<PendingOrder> order = await _repo.UserViewPendingMenuList(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region show approved order
        [HttpGet("UserViewApprovedOrder")]
        public async Task<ActionResult> UserViewApprovedOrderDetails(int id)
        {
            try
            {
                List<PendingOrder> order = await _repo.UserViewApprovedMenuList(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
