using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.IRepo
{
    public interface IApprovedUserRepo
    {
        Task<DataTable> GetAllUserDetails();
        Task<ApprovedUsersModel> AddApprovedUsers(ApprovedUsersModel approvedUsers);
        /*Task<DataTable> GetOnlyUserDetails();
        Task<DataTable> GetOnlyVendorDetails();
        Task<ApprovedUsersModel> UpdateUserDetails(ApprovedUsersModel user);
        Task<string> DeleteUserDetails(int id, string email);
        Task<ApprovedUsersModel> GetUserInfoByEmail(ApprovedUsersModel email);
        //Task<DataTable> GetUserInfoByEmail(ApprovedUsersModel user);

        //no of counts
        Task<int> TotalapprovedHotel();
        Task<int> TotalapprovedBranchl();
        Task<int> TotalapprovedUser();
        Task<int> TotalapprovedVendor();
        Task<int> TotalpendingUser();
        Task<int> Totalpendingvendor();
        Task<List<ApprovedUsersModel>> GetNewArrivals();
        //updat pass
        Task<ApprovedUsersModel> UpdatePassword(ApprovedUsersModel user);
        Task<object> StatusApproved(int orderId, string role);
        Task<List<AppoveOrder>> GetOrderDetails();*/


    }
}
