using FoodOrderDAL.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrderDAL.Repositories.IRepo;

namespace FoodOrderDAL.Repositories.ServiceRepo
{
    public class AddMenuRepo : IAddMenuRepo
    {
        private readonly SqlConnection _connection;
        public AddMenuRepo(IConfiguration configuration)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
        }

        //insert branch
        public async Task<MenuDetailsModel> AddMenu(MenuDetailsModel menu)
        {
            try
            {
                if (await MenuExistsInHotelBranch(menu.MenuItems, menu.HotelBranchId))
                {
                    throw new InvalidOperationException(menu.MenuItems + " already exists in this hotel branch.");
                }

                using (SqlCommand command = new SqlCommand("AddMenuItems", _connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@menuName", menu.MenuItems);
                    command.Parameters.AddWithValue("@menuItemQuantity", menu.MenuQuantity);
                    command.Parameters.AddWithValue("@menuPrice", menu.Price);
                    command.Parameters.AddWithValue("@hotelBranchId", menu.HotelBranchId);

                    await _connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return menu;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private async Task<bool> MenuExistsInHotelBranch(string menuName, int hotelBranchId)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM MenuDetails WHERE MenuItems = @menuName AND HotelBranchId = @hotelBranchId",_connection);
            command.Parameters.AddWithValue("@menuName", menuName);
            command.Parameters.AddWithValue("@hotelBranchId", hotelBranchId);
            await _connection.OpenAsync();
            int count = (int)await command.ExecuteScalarAsync();
            await _connection.CloseAsync();
            return count > 0;
        }

        //read menu items
        public async Task<List<MenuDetailsModel>> GetMenuDetials()
        {
            List<MenuDetailsModel> menu = new();
            SqlCommand cmd = new SqlCommand("viewMenuItems", _connection);
            await _connection.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                menu.Add(new MenuDetailsModel
                {
                   Id = reader.GetInt32(0),
                   MenuItems = reader.GetString(1),
                   MenuQuantity = reader.GetInt32(2),
                   Price = Convert.ToDouble( reader.GetDecimal(3)),
                   HotelBranchId = reader.GetInt32(4)
                });
            }
            return menu;
        }

        // update menu
        public async Task<MenuDetailsModel> UpdateMenu(MenuDetailsModel menu)
        {
            using (SqlCommand sql = new SqlCommand("UpdatemenuDetails", _connection))
            {
                await _connection.OpenAsync();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@itemId", menu.Id);
                sql.Parameters.AddWithValue("@menuName", menu.MenuItems);
                sql.Parameters.AddWithValue("@menuItemQuantity", menu.MenuQuantity);
                sql.Parameters.AddWithValue("@menuItemPrice", menu.Price);
                sql.Parameters.AddWithValue("@hotelBranchId", menu.HotelBranchId);
                int affected = await sql.ExecuteNonQueryAsync();
                if (affected > 0)
                {
                    return menu;
                }
                else
                {
                    throw new InvalidOperationException("No rows were affected during update operation.");
                }
            }
        }

        //delete menu
        public async Task<string> DeleteMenuDetails(int id, int hotelbranchId)
        {
            using (SqlCommand deletemenu = new SqlCommand("DeleteMenuItems", _connection))
            {
                await _connection.OpenAsync();
                deletemenu.CommandType = CommandType.StoredProcedure;
                deletemenu.Parameters.AddWithValue("@menuId", id);
                deletemenu.Parameters.AddWithValue("@hotelbranchId", hotelbranchId);
                int rowsAffected = await deletemenu.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    return "MenuItem Deleted Successfully";
                }
                else
                {
                    return "No Menu deleted. Possibly invalid MenuId or insufficient permissions.";
                }
            }
        }

    }
}
