using FoodOrderDAL.Models;
using FoodOrderDAL.Repositories.IRepo;
using MailKit.Search;
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
    public class OrderRepo : IOrderRepo
    {
        private readonly SqlConnection _connection;
        public OrderRepo(IConfiguration configuration)
        {
            string? createConnection = configuration.GetConnectionString("DatasourceConnect");
            _connection = new SqlConnection(createConnection);
        }

        public async Task<OrdersModel> AddOrders(OrdersModel Orders)
        {
            SqlCommand command = new SqlCommand("AddOrder", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId",Orders.ApprovedUsersId);
            command.Parameters.AddWithValue("@hotelbracnhId", Orders.HotelBranchId);
            command.Parameters.AddWithValue("@menudetailId", Orders.MenuDetailsId);
            command.Parameters.AddWithValue("@quantityCount", Orders.QuantityOfOrder);
            command.Parameters.AddWithValue("@totalPrice", Orders.Price);
            command.Parameters.AddWithValue("@orderStatus", Orders.OrderStatus);
            await _connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
            return Orders;
        }
    }
}
