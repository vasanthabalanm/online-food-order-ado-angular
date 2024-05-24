using FoodOrderDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Repositories.IRepo
{
    public interface IOrderRepo
    {

        Task<OrdersModel> AddOrders(OrdersModel Orders);

    }
}
