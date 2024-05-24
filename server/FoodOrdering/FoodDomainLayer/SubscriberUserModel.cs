using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomainLayer
{
    public class SubscriberUserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string subscriberEmail { get; set; } = string.Empty;
        public string SubscriberPassword { get; set; } = string.Empty; 
        public string SubscriberRole { get; set;} = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public string SubscriberPhone { get; set; } = string.Empty;
        public string SubscriberLocation { get; set; } = string.Empty;
    }
}
