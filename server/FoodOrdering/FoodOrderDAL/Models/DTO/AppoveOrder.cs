using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models.DTO
{
    public class AppoveOrder
    {
        public int OrderId { get; set; }
        public string MenuItems { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public int QuantityOfOrder { get; set; }

        [DataType(DataType.Currency)]
        public double TotalPrice { get; set; }
        public string OrderStatus { get; set; } = string.Empty;

    }
}
