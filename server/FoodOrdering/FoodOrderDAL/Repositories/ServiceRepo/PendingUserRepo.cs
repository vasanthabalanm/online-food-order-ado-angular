using FoodOrderDAL.Models;
using FoodOrderDAL.Repositories.IRepo;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.ServiceRepo
{
    public class PendingUserRepo:IPendingUserRepo
    {
        //private readonly IMailRepo _mailRepo;
        private readonly SqlConnection _connection;
        public PendingUserRepo(IConfiguration configuration)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
        }

        //register user
        public async Task<PendingUserModel> AddPendingUsers(PendingUserModel pendingUsers)
        {
            if (await EmailExistsInPendingUsers(pendingUsers.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            if (await EmailExistsInApprovedUsers(pendingUsers.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            SqlCommand command = new SqlCommand("PendingUsersList", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", pendingUsers.UserName);
            command.Parameters.AddWithValue("@email", pendingUsers.Email);
            command.Parameters.AddWithValue("@password", pendingUsers.UserPassword);
            command.Parameters.AddWithValue("@userRole", pendingUsers.UserRole);
            command.Parameters.AddWithValue("@userPhone", pendingUsers.UserPhone);
            command.Parameters.AddWithValue("@userLocation", pendingUsers.UserLocation);
            command.Parameters.AddWithValue("@temppass", pendingUsers.TempPassword);
            await _connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return pendingUsers;
        }
        private async Task<bool> EmailExistsInApprovedUsers(string email)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM ApprovedUsers WHERE Email = @email", _connection);
            command.Parameters.AddWithValue("@email", email);

            await _connection.OpenAsync();
            int count = (int)await command.ExecuteScalarAsync();
            await _connection.CloseAsync();

            return count > 0;
        }
        private async Task<bool> EmailExistsInPendingUsers(string email)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PendingUser WHERE Email = @email", _connection);
            command.Parameters.AddWithValue("@email", email);

            await _connection.OpenAsync();
            int count = (int)await command.ExecuteScalarAsync();
            await _connection.CloseAsync();

            return count > 0;
        }

        //display only users
        public async Task<List<PendingUserModel>> GetOnlyPendingUserDetails()
        {
            List<PendingUserModel> pendingUsers = new();
            SqlCommand cmd = new("exec PendingallUser", _connection);
            await _connection.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                pendingUsers.Add(new PendingUserModel()
                {
                    Id = reader.GetInt32(0),
                    UserName = reader.GetString(1),
                    Email = reader.GetString(2),
                    UserRole = reader.GetString(4),
                    UserPhone = reader.GetString(5),
                    UserLocation = reader.GetString(6)
                });
            }
            return pendingUsers;
        }

        //delete pending user
        public async Task<string> DeletePendingUserDetails(int id)
        {
            SqlCommand sql = new SqlCommand("DeletePendingUSers", _connection);
            await _connection.OpenAsync();
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@pendingId", id);
            await sql.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return "Pending User Deleting Successfully";
        }

        //register vendor
        public async Task<PendingUserModel> AddPendingVendor(PendingUserModel pendingVendor)
        {
            if (await EmailExistsInPendingUsers(pendingVendor.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            if (await EmailExistsInApprovedUsers(pendingVendor.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }
            SqlCommand command = new SqlCommand("PendingVendorList", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@username", pendingVendor.UserName);
            command.Parameters.AddWithValue("@email", pendingVendor.Email);
            command.Parameters.AddWithValue("@password", pendingVendor.UserPassword);
            command.Parameters.AddWithValue("@userRole", pendingVendor.UserRole);
            command.Parameters.AddWithValue("@userPhone", pendingVendor.UserPhone);
            command.Parameters.AddWithValue("@userLocation", pendingVendor.UserLocation);
            command.Parameters.AddWithValue("@temppass", pendingVendor.TempPassword);
            await _connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return pendingVendor;
        }

        //display only vendor
        public async Task<List<PendingUserModel>> GetOnlyPendingVendorDetails()
        {
            List<PendingUserModel> pendingVendor = new();
            SqlCommand cmd = new("exec PendingAllVendor", _connection);
            await _connection.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                pendingVendor.Add(new PendingUserModel()
                {
                    Id = reader.GetInt32(0),
                    UserName = reader.GetString(1),
                    Email = reader.GetString(2),
                    UserRole = reader.GetString(4),
                    UserPhone = reader.GetString(5),
                    UserLocation = reader.GetString(6)
                });
            }
            return pendingVendor;
        }

        //delete pending Vendor
        public async Task<string> DeletePendingVendorDetails(int id)
        {
            SqlCommand sql = new SqlCommand("DeletePendingVendors", _connection);
            await _connection.OpenAsync();
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@pendingId", id);
            await sql.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return "Pending Vendor Deleting Successfully";
        }


    }
}
