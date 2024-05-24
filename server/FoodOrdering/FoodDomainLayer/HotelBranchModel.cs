using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomainLayer
{
    public class HotelBranchModel
    {
        public int Id { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string BranchLocation { get; set; } = string.Empty;
        public string BranchPhoneNumber {  get; set; } = string.Empty;
        public int OwnerOfHotel { get; set; }
    }
}
