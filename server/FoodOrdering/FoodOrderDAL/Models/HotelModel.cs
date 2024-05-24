using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models
{
    public class HotelModel
    {
        public int Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public int ApprovedUsersId { get; set; }
    }
}
