using FoodOrderDAL.Models;
using FoodOrderDAL.Models.DTO;
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
    public class UserViewMenusRepo : IUserViewMenuRepo
    {
        private readonly SqlConnection _connection;

        #region Constructor Call with Db connection
        /// <summary>
        /// Constructor Call with Db connection
        /// </summary>
        /// <param name="configuration"></param>
        public UserViewMenusRepo(IConfiguration configuration)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
        }
        #endregion

        #region View Menu List
        /// <summary>
        /// To view the all menu list
        /// </summary>
        /// <returns>List of return menu list</returns>
        public async Task<List<UserViewMenuDTO>> UserViewMenuList()
        {
            try
            {
                List<UserViewMenuDTO> menu = new();
                SqlCommand sql = new SqlCommand("ShowMenuDetail", _connection);
                await _connection.OpenAsync();
                SqlDataReader reader = await sql.ExecuteReaderAsync();
                while (reader.Read())
                {
                    menu.Add(new UserViewMenuDTO
                    {
                        MenuItemId = reader.GetInt32(0),
                        MenuItems = reader.GetString(1),
                        Price = Convert.ToDouble(reader.GetDecimal(2)),
                        BranchName = reader.GetString(3),
                        BranchLocation = reader.GetString(4),
                        HotelBranchId = reader.GetInt32(5)
                    });
                }
                return menu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region filter by location and menuitem name 
        /// <summary>
        /// filter by location and menuitem name 
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="branchLocation"></param>
        /// <returns>return the respective value</returns>
        public async Task<UserViewMenuDTO> GetMenuFromLocation(string menuName,string branchLocation)
        {
            UserViewMenuDTO menus = null;
            await _connection.OpenAsync();
            SqlCommand sql = new SqlCommand("filterMenu", _connection);
            sql.CommandType = CommandType.StoredProcedure;
            sql.Parameters.AddWithValue("@menuItems", menuName);
            sql.Parameters.AddWithValue("@branchLocation", branchLocation);
            SqlDataReader reader = await sql.ExecuteReaderAsync();
            {
                if (await reader.ReadAsync())
                {
                    menus = new UserViewMenuDTO
                    {
                        MenuItemId = reader.GetInt32(0),
                        MenuItems = reader.GetString(1),
                        Price = Convert.ToDouble(reader.GetDecimal(2)),
                        BranchName = reader.GetString(3),
                        BranchLocation = reader.GetString(4),
                        HotelBranchId = reader.GetInt32(5)
                    };
                    return menus;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region pending OrderList user point of view
        public async Task<List<PendingOrder>> UserViewPendingMenuList(int id)
        {
            try
            {
                List<PendingOrder> order = new();
                SqlCommand sql = new SqlCommand("UsersOrders", _connection);
                await _connection.OpenAsync();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@userId", id);
                SqlDataReader reader = await sql.ExecuteReaderAsync();
                while (reader.Read())
                {
                    order.Add(new PendingOrder
                    {
                        Id = reader.GetInt32(0),
                        QuantityOfOrder = reader.GetInt32(1),
                        OrderStatus = reader.GetString(2),
                        TotalPrice = Convert.ToDouble(reader.GetDecimal(3)),
                        MenuItems = reader.GetString(4)
                       
                    });
                }
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region approved OrderList user point of view
        public async Task<List<PendingOrder>> UserViewApprovedMenuList(int id)
        {
            try
            {
                List<PendingOrder> order = new();
                SqlCommand sql = new SqlCommand("ApprovedUsersOrders", _connection);
                await _connection.OpenAsync();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@userId", id);
                SqlDataReader reader = await sql.ExecuteReaderAsync();
                while (reader.Read())
                {
                    order.Add(new PendingOrder
                    {
                        Id = reader.GetInt32(0),
                        QuantityOfOrder = reader.GetInt32(1),
                        OrderStatus = reader.GetString(2),
                        TotalPrice = Convert.ToDouble(reader.GetDecimal(3)),
                        MenuItems = reader.GetString(4)
                    });
                }
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
