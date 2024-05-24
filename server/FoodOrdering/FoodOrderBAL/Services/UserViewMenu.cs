using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models.DTO;
using FoodOrderDAL.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Services
{
    public class UserViewMenu : IUserViewMenu
    {
        public readonly IUserViewMenuRepo _repo;

        #region Constructor
        /// <summary>
        /// Constructor - initialize value
        /// </summary>
        /// <param name="repo"></param>
        public UserViewMenu(IUserViewMenuRepo repo)
        {
            _repo = repo;
        }
        #endregion

        #region To view Menu List
        public async Task<List<UserViewMenuDTO>> UserViewMenuList()
        {
            return await _repo.UserViewMenuList();
        }
        #endregion

        #region filter by location and by menuitem name
        public async Task<UserViewMenuDTO> GetMenuFromLocation(string menuName, string branchLocation)
        {
            
            return await _repo.GetMenuFromLocation(menuName, branchLocation);
        }
        #endregion

        #region To view pending menu form the user view
        public async Task<List<PendingOrder>> UserViewPendingMenuList(int id)
        {
            return await _repo.UserViewPendingMenuList(id);
        }
        #endregion

        #region To view Approving menu form the user view
        public async Task<List<PendingOrder>> UserViewApprovedMenuList(int id)
        {
            return await _repo.UserViewApprovedMenuList(id);
        }
        #endregion
    }
}
