using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models.DTO
{
    public class UserViewMenuDTO
    {
        public string MenuItems { get; set; } = string.Empty;

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public int HotelBranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchLocation { get; set; } = string.Empty;
        public int MenuItemId { get; set; }

    }
}
