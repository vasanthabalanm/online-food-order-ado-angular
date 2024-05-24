using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDomainLayer
{
    public class OrderCartDetailsModel
    {
        public int Id { get; set;}
        public int SubscriberApprovedId { get; set;}
        public int OrderedBranchId { get; set;}
        public int OrderedMenuId { get; set;}
        public int OrderCount { get; set;}
        public SqlMoney TotalPrice { get; set;}
    }
}
