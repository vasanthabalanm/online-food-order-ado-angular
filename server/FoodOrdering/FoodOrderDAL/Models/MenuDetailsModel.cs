using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models
{
    public class MenuDetailsModel
    {
        public int Id { get; set; }
        public string MenuItems { get; set; } = string.Empty;
        public int MenuQuantity {  get; set; }

        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public int HotelBranchId { get; set; }
    }
}
