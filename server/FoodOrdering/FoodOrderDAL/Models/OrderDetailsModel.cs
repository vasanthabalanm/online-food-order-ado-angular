﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderDAL.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public int MenuDetailsId { get; set; }
        public int QuantityOfOrder { get; set; }
    }
}
