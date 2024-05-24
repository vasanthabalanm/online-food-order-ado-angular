using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderBAL.Interfaces
{
    public interface IOrder
    {
        Task<OrdersModel> AddOrders(OrdersModel Orders);
    }
}
