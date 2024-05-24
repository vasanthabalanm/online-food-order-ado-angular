using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.IRepo
{
    public interface IUserViewMenuRepo
    {
        Task<List<UserViewMenuDTO>> UserViewMenuList();
        Task<UserViewMenuDTO> GetMenuFromLocation(string menuName, string branchLocation);
        //show pending
        Task<List<PendingOrder>> UserViewPendingMenuList(int id);
        //show approved
        Task<List<PendingOrder>> UserViewApprovedMenuList(int id);
    }
}
