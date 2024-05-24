using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomainLayer
{
    public class MenuDetailsModel
    {
        public int Id { get; set; }
        public string MenuItems { get; set; } = string.Empty;
        public SqlMoney Price {  get; set; }
        public int HotelBranchId { get; set; }
    }
}
