using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models
{
    public class OrdersModel
    {
        public int Id { get; set; }
        public int ApprovedUsersId { get; set; }
        public int HotelBranchId { get; set; }
        public int MenuDetailsId {  get; set; }
        public int QuantityOfOrder { get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
    }
}
