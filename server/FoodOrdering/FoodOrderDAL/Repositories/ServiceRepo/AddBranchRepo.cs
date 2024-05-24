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
using System.Xml.Linq;

namespace FoodOrderDAL.Repositories.ServiceRepo
{
    public class AddBranchRepo : IAddBranchRepo
    {
        private readonly SqlConnection _connection;
        public AddBranchRepo(IConfiguration configuration)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
        }

        //insert branch
        public async Task<HotelBranchModel> AddBranch(HotelBranchModel hotel)
        {
            try
            {
                if (await HotelBranchexistInHotel(hotel.BranchName, hotel.HotelId))
                {
                    throw new InvalidOperationException(hotel.BranchName + " already exists in this hotel.");
                }
                SqlCommand command = new SqlCommand("AddHotelBranch", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@branchName", hotel.BranchName);
                command.Parameters.AddWithValue("@branchLocation", hotel.BranchLocation);
                command.Parameters.AddWithValue("@branchPhoneNumber", hotel.BranchPhoneNumber);
                command.Parameters.AddWithValue("@hotelId", hotel.HotelId);
                await _connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
                await _connection.CloseAsync();
                return hotel;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> HotelBranchexistInHotel(string bname, int hotelId)
        {
            SqlCommand command = new SqlCommand(
                "SELECT COUNT(*) FROM HotelBranch WHERE BranchName = @branchName AND HotelId = @hotelId",
                _connection
            );
            command.Parameters.AddWithValue("@branchName", bname);
            command.Parameters.AddWithValue("@hotelId", hotelId);

            await _connection.OpenAsync();
            int count = (int)await command.ExecuteScalarAsync();
            await _connection.CloseAsync();

            return count > 0;
        }

        // update hotel by admin
        public async Task<HotelBranchModel> UpdateHotel(HotelBranchModel hotel)
        {
            try
            {
                if (await HotelBranchExistInHotelForUpdate(hotel.Id, hotel.BranchName, hotel.HotelId))
                {
                    throw new InvalidOperationException(hotel.BranchName + " already exists in this hotel.");
                }

                SqlCommand sql = new SqlCommand("UpdateBranchByAdmin", _connection);
                await _connection.OpenAsync();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@branchId", hotel.Id);
                sql.Parameters.AddWithValue("@branchname", hotel.BranchName);
                sql.Parameters.AddWithValue("@branchLocation", hotel.BranchLocation);
                sql.Parameters.AddWithValue("@branchPhoneNumber", hotel.BranchPhoneNumber);
                sql.Parameters.AddWithValue("@hotelId", hotel.HotelId);
                await sql.ExecuteNonQueryAsync();
                await _connection.CloseAsync();

                return hotel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> HotelBranchExistInHotelForUpdate(int branchId, string branchName, int hotelId)
        {
            SqlCommand command = new SqlCommand(
                  "SELECT * FROM HotelBranch WHERE BranchName = @branchname AND HotelId = @hotelId",
                  _connection
              );
            command.Parameters.AddWithValue("@branchName", branchId);
            command.Parameters.AddWithValue("@hotelId", hotelId);

            await _connection.OpenAsync();
            int count = (int)await command.ExecuteScalarAsync();
            await _connection.CloseAsync();

            return count > 0;
        }



        //read only by admin to get the hotelbrnaches
        public async Task<List<HotelBranchModel>> GethotelDetials()
        {
            try
            {
                List<HotelBranchModel> hotel = new();
                SqlCommand cmd = new SqlCommand("exec ListAllBranches", _connection);
                await _connection.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    hotel.Add(new HotelBranchModel
                    {
                        Id = reader.GetInt32(0),
                        BranchName = reader.GetString(1),
                        BranchLocation = reader.GetString(2),
                        BranchPhoneNumber = reader.GetString(3),
                        HotelId = reader.GetInt32(4)
                    });
                }
                return hotel;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to retrieve hotel details.", ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    await _connection.CloseAsync();
            }
        }


        //delete hotelbranch
        public async Task<string> DeleteHotelbranch(int id, int hotelid)
        {
            try
            {
                SqlCommand deletehotel = new SqlCommand("DeleteApprovedHotelBranch", _connection);
                await _connection.OpenAsync();
                deletehotel.CommandType = CommandType.StoredProcedure;
                deletehotel.Parameters.AddWithValue("@id", id);
                deletehotel.Parameters.AddWithValue("@hotelId", hotelid);
                int rowsAffected = await deletehotel.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "Hotel branch Deleting Successfully";
                }
                else
                {
                    return "No hotelbranch deleted. Possibly invalid HotelId or insufficient permissions.";
                }
            }
            catch (Exception ex)
            {
                return $"Error deleting hotel branch: {ex.Message}";
            }
        }

    }
}
