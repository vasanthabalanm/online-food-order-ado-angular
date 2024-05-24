using FoodOrderBAL.HelpModule;
using FoodOrderBAL.Interfaces;
using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
using FoodOrderDAL.Repositories.IRepo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Services
{
    public class ApprovedUserSerivce : IApprovedUser
    {
        private readonly IApprovedUserRepo _approvedUserRepo;
        private readonly IMailRepo _mailRepo;

        #region Intialize the values inside te constructor
        /// <summary>
        /// Intialize the values inside te constructor
        /// </summary>
        /// <param name="approvedUserRepo"></param>
        /// <param name="mailrepo"></param>
        public ApprovedUserSerivce(IApprovedUserRepo approvedUserRepo,IMailRepo mailrepo)
        {
            _approvedUserRepo = approvedUserRepo;
            _mailRepo = mailrepo;
        }
        #endregion

        #region To get all approved Users
        /// <summary>
        /// To get all approved Users
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApprovedUsersModel>> GetAllUserDetails()
        {
            List<ApprovedUsersModel> approvedUsers = new List<ApprovedUsersModel>();
            DataTable dataTable = await _approvedUserRepo.GetAllUserDetails();

            foreach (DataRow row in dataTable.Rows)
            {
                approvedUsers.Add(new ApprovedUsersModel
                {
                    Id = Convert.ToInt32(row[0]),
                    UserName = row[1].ToString(),
                    Email = row[2].ToString(),
                    UserPassword = row[3].ToString(),
                    UserRole = row[4].ToString(),
                    UserPhone = row[5].ToString(),
                    UserLocation = row[6].ToString(),
                    TempPassword = row[7].ToString()
                });
            }
            return approvedUsers;
        }
        #endregion

        #region To Add Approved Users by verify the mail Password
        /// <summary>
        /// To Add Approved Users by verify the mail Password
        /// </summary>
        /// <param name="approvedUsers"></param>
        /// <returns></returns>
        public async Task<ApprovedUsersModel> AddApprovedUsers(ApprovedUsersModel approvedUsers)
        {
            string tempPass = _mailRepo.SendMail(approvedUsers.Email);
            approvedUsers.TempPassword = tempPass;
            approvedUsers.UserPassword = PasswordCheck.HashPassword(tempPass);
            var result = await _approvedUserRepo.AddApprovedUsers(approvedUsers);
            return result;
        }
        #endregion
/*
        #region To Fetch the approved User Role Details
        /// <summary>
        /// To Fetch the approved User Role Details
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApprovedUsersModel>> GetOnlyUserDetails()
        {
            List<ApprovedUsersModel> approvedUsers = new List<ApprovedUsersModel>();
            DataTable dataTable = await _approvedUserRepo.GetOnlyUserDetails();

            foreach (DataRow row in dataTable.Rows)
            {
                approvedUsers.Add(new ApprovedUsersModel
                {
                    Id = Convert.ToInt32(row[0]),
                    UserName = row[1].ToString(),
                    Email = row[2].ToString(),
                    UserPassword = row[3].ToString(),
                    UserRole = row[4].ToString(),
                    UserPhone = row[5].ToString(),
                    UserLocation = row[6].ToString()
                });
            }
            return approvedUsers;
        }
        #endregion

        #region To fetch the Vendor role details
        /// <summary>
        /// To fetch the Vendor role details
        /// </summary>
        /// <returns>list of vendor roles details</returns>
        public async Task<List<ApprovedUsersModel>> GetOnlyVendorDetails()
        {
            List<ApprovedUsersModel> approvedUsers = new List<ApprovedUsersModel>();
            DataTable dataTable = await _approvedUserRepo.GetOnlyVendorDetails();
            foreach (DataRow row in dataTable.Rows)
            {
                approvedUsers.Add(new ApprovedUsersModel
                {
                    Id = Convert.ToInt32(row[0]),
                    UserName = row[1].ToString(),
                    Email = row[2].ToString(),
                    UserPassword = row[3].ToString(),
                    UserRole = row[4].ToString(),
                    UserPhone = row[5].ToString(),
                    UserLocation = row[6].ToString()
                });
            }
            return approvedUsers;
        }
        #endregion

        //check
        #region Update the user profile
        /// <summary>
        /// Update the user profile
        /// </summary>
        /// <param name="user"></param>
        /// <returns>the updated values</returns>
        public async Task<ApprovedUsersModel> UpdateUserDetails(ApprovedUsersModel user)
        {
            return await _approvedUserRepo.UpdateUserDetails(user);
        }
        #endregion

        #region Delete the all approved User
        /// <summary>
        /// Delete the all approved User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns>return deleted successfully</returns>
        public async Task<string> DeleteUserDetails(int id, string email)
        {
            return await _approvedUserRepo.DeleteUserDetails(id, email);
        }
        #endregion

        #region Get Information from by respective maik id's
        /// <summary>
        /// Get Information from by respective maik id's
        /// </summary>
        /// <param name="user"></param>
        /// <returns>return information details</returns>
        /// <exception cref="Exception"></exception>
        public async Task<ApprovedUsersModel> GetUserInfoByEmail(ApprovedUsersModel user)
        {

            var result = await _approvedUserRepo.GetUserInfoByEmail(user);

            if (!PasswordCheck.VerifyPassword(user.UserPassword, result.UserPassword))
            {
                throw new Exception("password is incorrect");
            }
            return result;
            *//*List<ApprovedUsersModel> approvedUsers = new List<ApprovedUsersModel>();
            DataTable dataTable = await _approvedUserRepo.GetUserInfoByEmail(user);

            foreach (DataRow row in dataTable.Rows)
            {
                approvedUsers.Add(new ApprovedUsersModel
                {
                    Id = Convert.ToInt32(row[0]),
                    UserName = row[1].ToString(),
                    Email = row[2].ToString(),
                    UserPassword = row[3].ToString(),
                    UserRole = row[4].ToString(),
                    TempPassword = row[7].ToString()
                });
            }
            return approvedUsers;*//*
        }
        #endregion

        #region Display total Number of Hotel
        public async Task<int> TotalapprovedHotel()
        {
            return await _approvedUserRepo.TotalapprovedHotel();
        }
        #endregion

        #region Display number of branch
        public async Task<int> TotalapprovedBranchl()
        {
            return await _approvedUserRepo.TotalapprovedBranchl();
        }
        #endregion

        #region Display total approved User
        public async Task<int> TotalapprovedUser()
        {
            return await _approvedUserRepo.TotalapprovedUser();
        }
        #endregion

        #region Display Approved Vendor
        public async Task<int> TotalapprovedVendor()
        {
            return await _approvedUserRepo.TotalapprovedVendor();
        }
        #endregion

        #region Display Total pending user
        public async Task<int> TotalpendingUser()
        {
            return await _approvedUserRepo.TotalpendingUser();
        }
        #endregion

        #region Display Total Pending vendor
        public async Task<int> Totalpendingvendor()
        {
            return await _approvedUserRepo.Totalpendingvendor();
        }
        #endregion

        #region Display new arrivals
        public async Task<List<ApprovedUsersModel>> GetNewArrivals()
        {
            return await _approvedUserRepo.GetNewArrivals();
        }
        #endregion

        #region update password for respecive new logins
        public async Task<ApprovedUsersModel> UpdatePassword(ApprovedUsersModel user)
        {
            user.UserPassword = PasswordCheck.HashPassword(user.UserPassword);
            return await _approvedUserRepo.UpdatePassword(user);
        }
        #endregion

        #region Change the Order status change for pending order
        public async Task<object> StatusApproved(int orderId, string role)
        {
            return await _approvedUserRepo.StatusApproved(orderId, role);
        }
        #endregion

        #region Display the all order details
        public async Task<List<AppoveOrder>> GetOrderDetails()
        {
            return await _approvedUserRepo.GetOrderDetails();
        }
        #endregion*/
    }
}
