using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomainLayer
{
    public class ApprovedUsersModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserPassword { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        public string UserPhone { get; set; } = string.Empty;
        public string UserLocation { get; set; } = string.Empty;

    }
}
