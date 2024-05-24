using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using FoodOrderDAL.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Services
{
    public class PendingUserService : IPendingUser
    {
        private readonly IPendingUserRepo _repo;
        public PendingUserService(IPendingUserRepo repo)
        {
            _repo = repo;
        }

        public async Task<PendingUserModel> AddPendingUsers(PendingUserModel pendingUsers)
        {
            return await _repo.AddPendingUsers(pendingUsers);
        }

        public async Task<List<PendingUserModel>> GetOnlyPendingUserDetails()
        {
            return await _repo.GetOnlyPendingUserDetails();
        }

        public async Task<string> DeletePendingUserDetails(int id)
        {
            return await _repo.DeletePendingUserDetails(id);
        }

        public async Task<PendingUserModel> AddPendingVendor(PendingUserModel pendingVendor)
        {
            return await _repo.AddPendingVendor(pendingVendor);
        }

        public async Task<List<PendingUserModel>> GetOnlyPendingVendorDetails()
        {
            return await _repo.GetOnlyPendingVendorDetails();
        }

        public async Task<string> DeletePendingVendorDetails(int id)
        {
            return await _repo.DeletePendingVendorDetails(id);
        }
    }
}
