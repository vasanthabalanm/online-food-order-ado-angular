using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models.DTO
{
    public class PendingOrder
    {
        public int Id { get; set; }
        public int QuantityOfOrder { get; set; }
        public string OrderStatus { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
        public string MenuItems { get; set; } = string.Empty;
    }
}
