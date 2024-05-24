using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Interfaces
{
    public interface IApprovedUser
    {
        Task<List<ApprovedUsersModel>> GetAllUserDetails();
        Task<ApprovedUsersModel> AddApprovedUsers(ApprovedUsersModel approvedUsers);
        /*Task<List<ApprovedUsersModel>> GetOnlyUserDetails();
        Task<List<ApprovedUsersModel>> GetOnlyVendorDetails();
        Task<ApprovedUsersModel> UpdateUserDetails(ApprovedUsersModel user);
        Task<string> DeleteUserDetails(int id, string email);
        Task<ApprovedUsersModel> GetUserInfoByEmail(ApprovedUsersModel email);

        //display no of counts
        Task<int> TotalapprovedHotel();
        Task<int> TotalapprovedBranchl();
        Task<int> TotalapprovedUser();
        Task<int> TotalapprovedVendor();
        Task<int> TotalpendingUser();
        Task<int> Totalpendingvendor();
        Task<List<ApprovedUsersModel>> GetNewArrivals();

        //update pass
        Task<ApprovedUsersModel> UpdatePassword(ApprovedUsersModel user);

        //mail triger to order
        Task<object> StatusApproved(int orderId, string role);

        Task<List<AppoveOrder>> GetOrderDetails();*/

    }
}
