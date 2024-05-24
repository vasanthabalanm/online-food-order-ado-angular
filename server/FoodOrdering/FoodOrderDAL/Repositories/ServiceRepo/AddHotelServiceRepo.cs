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
    public class AddHotelServiceRepo : IAddHotelRepo
    {
        private readonly SqlConnection _connection;
        public AddHotelServiceRepo(IConfiguration configuration)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
        }

        //add hotel
        public async Task<HotelModel> AddHotel(HotelModel hotel)
        {
            try
            {
                SqlCommand command = new SqlCommand("InsertHotel", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@HotelName", hotel.HotelName);
                command.Parameters.AddWithValue("@ApprovedUsersId", hotel.ApprovedUsersId);
                await _connection.OpenAsync();

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return hotel;
                }
                else
                {
                    throw new InvalidOperationException("Failed to add hotel. Possibly invalid data or insufficient permissions.");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to add hotel.", ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    await _connection.CloseAsync();
            }
        }


        //read all hotel
        public async Task<List<HotelModel>> GethotelDetials()
        {
            List<HotelModel> hotel = new();
            SqlCommand cmd = new SqlCommand("GetallHotel", _connection);
            await _connection.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                hotel.Add(new HotelModel
                {
                    Id = reader.GetInt32(0),
                    HotelName = reader.GetString(1),
                    ApprovedUsersId = reader.GetInt32(2)
                });
            }
            return hotel;
        }

        //delete hotel
        public async Task<string> DeleteHotelDetails(int id, string role)
        {
            try
            {
                SqlCommand deletehotel = new SqlCommand("DeleteHotel", _connection);
                await _connection.OpenAsync();
                deletehotel.CommandType = CommandType.StoredProcedure;
                deletehotel.Parameters.AddWithValue("@HotelId", id);
                deletehotel.Parameters.AddWithValue("@AdminRole", role);
                int rowsAffected = await deletehotel.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "Hotel Deleting Successfully";
                }
                else
                {
                    return "No hotel deleted. Possibly invalid HotelId or insufficient permissions.";
                }
            }
            catch (Exception ex)
            {
                return $"Error deleting hotel: {ex.Message}";
            }
        }


        //update hotel
        public async Task<HotelModel> UpdateHotel(HotelModel hotel)
        {
            SqlCommand alterhotel = new SqlCommand("AlterHotel", _connection);
            await _connection.OpenAsync();
            alterhotel.CommandType = CommandType.StoredProcedure;
            alterhotel.Parameters.AddWithValue("@hotelid", hotel.Id);
            alterhotel.Parameters.AddWithValue("@hotelname", hotel.HotelName);
            alterhotel.Parameters.AddWithValue("@approveduserId", hotel.ApprovedUsersId);
            int rowsAffected = await alterhotel.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                return hotel;
            }
            else
            {
                throw new ArgumentNullException("Update failed: No rows affected.");
            }
        }

    }
}
