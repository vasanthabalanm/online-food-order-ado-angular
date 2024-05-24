using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Interfaces
{
    public interface IPendingUser
    {
        Task<PendingUserModel> AddPendingUsers(PendingUserModel pendingUsers);
        Task<List<PendingUserModel>> GetOnlyPendingUserDetails();
        Task<string> DeletePendingUserDetails(int id);
        //vendor
        Task<PendingUserModel> AddPendingVendor(PendingUserModel pendingVendor);
        Task<List<PendingUserModel>> GetOnlyPendingVendorDetails();
        Task<string> DeletePendingVendorDetails(int id);
    }
}
