using FoodOrderDAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Interfaces
{
    public interface IUserViewMenu
    {
        Task<List<UserViewMenuDTO>> UserViewMenuList();
        //filter
        Task<UserViewMenuDTO> GetMenuFromLocation(string menuName, string branchLocation);

        //show pending
        Task<List<PendingOrder>> UserViewPendingMenuList(int id);
        //show approved
        Task<List<PendingOrder>> UserViewApprovedMenuList(int id);
    }
}
