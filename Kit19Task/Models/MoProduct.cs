﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kit19Task.Models
{
    public class MoProduct
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public DateTime MfgDate { get; set; }
        public string Category { get; set; }
    }
}