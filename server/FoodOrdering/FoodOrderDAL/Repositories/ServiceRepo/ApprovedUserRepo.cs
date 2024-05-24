using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
using FoodOrderDAL.Repositories.IRepo;
using FoodOrderDAL.USP_StoreProcedure;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.ServiceRepo
{
    public class ApprovedUserRepo : IApprovedUserRepo
    {
        private readonly NpgsqlConnection _connection;
        private readonly IMailRepo _mailRepo;

        public ApprovedUserRepo(IConfiguration configuration, IMailRepo repo)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new NpgsqlConnection(createConnection);
            _mailRepo = repo;
        }

        public async Task<DataTable> GetAllUserDetails()
        {
            DataTable dataTable = new DataTable();
            using (NpgsqlCommand cmd = new NpgsqlCommand("select * from OverallUsers();", _connection))
            {
                await _connection.OpenAsync();
                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                dataTable.Load(reader);
            }
            return dataTable;
        }

        public async Task<ApprovedUsersModel> AddApprovedUsers(ApprovedUsersModel approvedUsers)
        {
            NpgsqlCommand command = new NpgsqlCommand ("ApproveUsersList", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("username", approvedUsers.UserName);
            command.Parameters.AddWithValue("email", approvedUsers.Email);
            command.Parameters.AddWithValue("userPassword", approvedUsers.UserPassword);
            command.Parameters.AddWithValue("userRole", approvedUsers.UserRole);
            command.Parameters.AddWithValue("userPhone", approvedUsers.UserPhone);
            command.Parameters.AddWithValue("userLocation", approvedUsers.UserLocation);
            command.Parameters.AddWithValue("tempPass", approvedUsers.TempPassword);
            await _connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return approvedUsers;
        }

        /*private readonly SqlConnection _connection;
        private readonly IMailRepo _mailRepo;

        #region Intialize value inside the constructor 
        /// <summary>
        /// Intialize value inside the constructor 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="repo"></param>
        public ApprovedUserRepo(IConfiguration configuration,IMailRepo repo)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
            _mailRepo = repo;
        }
        #endregion

        #region To fetch the overall Approved Users
        /// <summary>
        /// To fetch All User
        /// </summary>
        public async Task<DataTable> GetAllUserDetails()
        {
            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(Custom_StoreProcedures.SP_TogetAllUsers, _connection))
            {
                await _connection.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return dataTable;
        }
        #endregion

        #region To insert Apporve users Value
        /// <summary>
        /// To add values to the approve users
        /// </summary>
        /// <param name="approvedUsers"></param>
        public async Task<ApprovedUsersModel> AddApprovedUsers(ApprovedUsersModel approvedUsers)
        {
            SqlCommand command = new SqlCommand("ApproveUsersList", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", approvedUsers.UserName);
            command.Parameters.AddWithValue("@email", approvedUsers.Email);
            command.Parameters.AddWithValue("@userPassword", approvedUsers.UserPassword);
            command.Parameters.AddWithValue("@userRole",approvedUsers.UserRole);
            command.Parameters.AddWithValue("@userPhone", approvedUsers.UserPhone);
            command.Parameters.AddWithValue("@userLocation", approvedUsers.UserLocation);
            command.Parameters.AddWithValue("@tempPass", approvedUsers.TempPassword);
            await _connection.OpenAsync();  
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return approvedUsers;
        }
        #endregion

        #region To get only user Roles   
        /// <summary>
        /// To get only user Roles
        /// </summary>
        /// <returns>list of user Roles</returns>
        public async Task<DataTable> GetOnlyUserDetails()
        {
            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand("exec ApprovesOnlyUsers", _connection))
            {
                await _connection.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return dataTable;
        }
        #endregion

        #region To get Only Vendor Role Details
        /// <summary>
        /// To get Only Vendor Role Details
        /// </summary>
        /// <returns>List of Vendor Role List</returns>
        public async Task<DataTable> GetOnlyVendorDetails()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new("exec ApprovedOnlyVendors", _connection))
            {
                await _connection.OpenAsync();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
        #endregion

        #region update approved users by their id
        /// <summary>
        /// Update the specific detail by verify the UniqueID
        /// </summary>
        /// <param name="user"></param>
        /// <returns>update approved users by their id</returns>
        public async Task<ApprovedUsersModel> UpdateUserDetails(ApprovedUsersModel user)
        {
            SqlCommand addUserInfo = new SqlCommand("UpdateUserProfile", _connection);
            await _connection.OpenAsync();
            addUserInfo.CommandType = CommandType.StoredProcedure;
            addUserInfo.Parameters.AddWithValue("@username", user.UserName);
            addUserInfo.Parameters.AddWithValue("@userphone", user.UserPhone);
            addUserInfo.Parameters.AddWithValue("@userLocation", user.UserLocation);
            addUserInfo.Parameters.AddWithValue("@id", user.Id);
            addUserInfo.Parameters.AddWithValue("@email", user.Email);
            await addUserInfo.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return user;
        }
        #endregion

        #region delete action only by admin both user , vendor
        /// <summary>
        /// delete action only by admin both user , vendor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns>String type</returns>
        public async Task<string> DeleteUserDetails(int id,string email)
        {
            SqlCommand addUserInfo = new SqlCommand("DeleteApprovedUSers", _connection);
            await _connection.OpenAsync();
            addUserInfo.CommandType = CommandType.StoredProcedure;
            addUserInfo.Parameters.AddWithValue("@approvedId", id);
            addUserInfo.Parameters.AddWithValue("@email", email);
            await addUserInfo.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return "Approved User Deleting Successfully";
        }
        #endregion
        //check
        #region login by verify the details 
        /// <summary>
        /// login by verify the details 
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Return the neccesary details</returns>
        public async Task<ApprovedUsersModel> GetUserInfoByEmail(ApprovedUsersModel user)
        {
            ApprovedUsersModel usersModel = null;
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("SELECT UserName,Email,UserRole,Id,UserPassword,TempPassword FROM ApprovedUsers WHERE Email = @email", _connection);
            sql.Parameters.AddWithValue("@email", user.Email);
            SqlDataReader reader = await sql.ExecuteReaderAsync();
            {
                if (await reader.ReadAsync())
                {
                    usersModel = new ApprovedUsersModel
                    {
                        UserName = reader.GetString(reader.GetOrdinal("UserName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        UserRole = reader.GetString(reader.GetOrdinal("UserRole")),
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        UserPassword = reader.GetString(reader.GetOrdinal("UserPassword")),
                        TempPassword = reader.GetString(reader.GetOrdinal("TempPassword"))
                    };
                }
            }
            return usersModel;

            *//* DataTable dataTable = new DataTable();
             using (SqlCommand cmd = new SqlCommand("SELECT UserName,Email,UserRole,Id,UserPassword,TempPassword FROM ApprovedUsers WHERE Email = @email", _connection))
             {
                 cmd.Parameters.AddWithValue("@email", user.Email);
                 await _connection.OpenAsync();
                 using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                 {
                     dataTable.Load(reader);
                 }
             }
             return dataTable;*//*
        }
        #endregion

        #region Display number of hotel
        /// <summary>
        /// Display number of hotel
        /// </summary>
        /// <returns>return int type</returns>
        public async Task<int> TotalapprovedHotel()
        {
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("select count(*) as TotalHotel from Hotel", _connection);
            int totalHotel = Convert.ToInt32(await sql.ExecuteScalarAsync());
            await _connection.CloseAsync();
            return totalHotel;
        }
        #endregion

        #region Display number of branch
        /// <summary>
        ///  Display number of branch
        /// </summary>
        /// <returns>return the int typye</returns>
        public async Task<int> TotalapprovedBranchl()
        {
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("select count(*) as TotalBranch from HotelBranch", _connection);
            int totalHotelbranch = Convert.ToInt32(await sql.ExecuteScalarAsync());
            await _connection.CloseAsync();
            return totalHotelbranch;
        }
        #endregion

        #region Number of approved vendor
        /// <summary>
        /// Number of approved vendor
        /// </summary>
        /// <returns>retutn int type</returns>
        public async Task<int> TotalapprovedUser()
        {
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("select count(*) as ApprovedUser from ApprovedUsers where UserRole = 'User' ", _connection);
            int totalappprovedUser = Convert.ToInt32(await sql.ExecuteScalarAsync());
            await _connection.CloseAsync();
            return totalappprovedUser;
        }
        #endregion

        #region Return Number of approved vendor
        /// <summary>
        /// Return Number of approved vendor
        /// </summary>
        /// <returns>retutn int type</returns>
        public async Task<int> TotalapprovedVendor()
        {
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("select count(*) as ApprovedUser from ApprovedUsers where UserRole = 'Vendor' ", _connection);
            int totalapprovedVendor = Convert.ToInt32(await sql.ExecuteScalarAsync());
            await _connection.CloseAsync();
            return totalapprovedVendor;
        }
        #endregion

        #region Number of pending user
        /// <summary>
        /// Number of pending user
        /// </summary>
        /// <returns>return int type</returns>
        public async Task<int> TotalpendingUser()
        {
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("select count(*) as PendingUser from PendingUser where UserRole = 'User' ", _connection);
            int totalPendinguser = Convert.ToInt32(await sql.ExecuteScalarAsync());
            await _connection.CloseAsync();
            return totalPendinguser;
        }
        #endregion

        #region Number of pending vendor
        /// <summary>
        /// Number of pending vendor
        /// </summary>
        /// <returns>return int type</returns>
        public async Task<int> Totalpendingvendor()
        {
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("select count(*) as PendingVendor from PendingUser where UserRole = 'Vendor' ", _connection);
            int totalPendingVendor = Convert.ToInt32(await sql.ExecuteScalarAsync());
            await _connection.CloseAsync();
            return totalPendingVendor;
        }
        #endregion

        #region List Of New Arrivals
        /// <summary>
        ///  List Of New Arrivals
        /// </summary>
        /// <returns>return the list of new arrivals</returns>
        public async Task<List<ApprovedUsersModel>> GetNewArrivals()
        {
            List<ApprovedUsersModel> usersList = new List<ApprovedUsersModel>();
            try
            {
                await _connection.OpenAsync();
                using (SqlCommand sql = new SqlCommand("GetNewArrivals", _connection))
                {
                    sql.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = await sql.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        ApprovedUsersModel user = new ApprovedUsersModel
                        {
                            UserName = reader.GetString(reader.GetOrdinal("UserName")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            UserRole = reader.GetString(reader.GetOrdinal("UserRole")),
                        };
                        usersList.Add(user);
                    }
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }

            return usersList;
        }
        #endregion

        #region update password For new Users (OneTime)
        /// <summary>
        /// Update password For new Users (OneTime)
        /// </summary>
        /// <param name="user"></param>
        /// <returns>return the update password in hash format</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<ApprovedUsersModel> UpdatePassword(ApprovedUsersModel user)
        {
            SqlCommand updatePassword = new SqlCommand("updatePass", _connection);
            await _connection.OpenAsync();
            updatePassword.CommandType = CommandType.StoredProcedure;
            updatePassword.Parameters.AddWithValue("@email", user.Email);
            updatePassword.Parameters.AddWithValue("@id", user.Id);
            updatePassword.Parameters.AddWithValue("@oldpass", user.TempPassword);
            updatePassword.Parameters.AddWithValue("@newpass", user.UserPassword);
            int affect = await updatePassword.ExecuteNonQueryAsync();
            if (affect > 0)
            {
                return user;
            }
            else
            {
                throw new ArgumentNullException("Update failed: No rows affected.");
            }
        }
        #endregion

        #region Need Trigger mail for approve order
        /// <summary>
        /// Send mail for approve
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="role"></param>
        /// <returns>Need Trigger mail for approve order</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<object> StatusApproved(int orderId, string role)
        {
            try
            {
                await _connection.OpenAsync();

                using (SqlCommand statusChange = new SqlCommand("ApporoveOrder", _connection))
                {
                    statusChange.CommandType = CommandType.StoredProcedure;
                    statusChange.Parameters.AddWithValue("@orderId", orderId);
                    statusChange.Parameters.AddWithValue("@userRole", role);

                    var result = await GetMailFromOrderId(orderId);
                    string email = result.ToString();

                    _mailRepo.SendOrderApproveMail(email);

                    int affectedRows = await statusChange.ExecuteNonQueryAsync();

                    if (affectedRows > 0)
                    {
                        return new { message = "Approved" };
                    }
                    else
                    {
                        throw new ArgumentNullException("Update failed: No rows affected.");
                    }
                }
            }
            finally
            {
                _connection.Close();
            }
        }


        // Get mail from order id
        public async Task<string> GetMailFromOrderId(int orderId)
        {
            SqlCommand getEmailCommand = new SqlCommand("getEmail", _connection);
                
                    getEmailCommand.CommandType = CommandType.StoredProcedure;
                    getEmailCommand.Parameters.AddWithValue("@orderID", orderId);

                    object result = await getEmailCommand.ExecuteScalarAsync();

                    if (result != null)
                    {
                        string email = result.ToString();
                        return email;
                    }
                    else
                    {
                        throw new ArgumentNullException("Email not found for the given order ID.");
                    }
        }
        #endregion

        #region GetAll Order Details
        /// <summary>
        /// GetAll Order Details
        /// </summary>
        /// <returns>return list of orders</returns>
        public async Task<List<AppoveOrder>> GetOrderDetails()
        {
            List<AppoveOrder> orders = new();
            SqlCommand cmd = new("exec sendemail", _connection);
            await _connection.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                orders.Add(new AppoveOrder
                {
                    OrderId = reader.GetInt32(0),
                    MenuItems = reader.GetString(1),
                    Email = reader.GetString(2),
                    BranchName = reader.GetString(3),
                    QuantityOfOrder = reader.GetInt32(4),
                    TotalPrice = Convert.ToDouble(reader.GetDecimal(5)),
                    OrderStatus = reader.GetString(6)
                });
            }
            return orders;
        }
        #endregion*/

    }
}
