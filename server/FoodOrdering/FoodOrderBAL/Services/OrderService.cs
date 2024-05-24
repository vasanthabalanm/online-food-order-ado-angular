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
    public class OrderService : IOrder
    {
        private readonly IOrderRepo _repo;
        public OrderService(IOrderRepo repo)
        {
            _repo = repo;
        }

        public async Task<OrdersModel> AddOrders(OrdersModel Orders)
        {
            return await _repo.AddOrders(Orders);
        }
    }
}
