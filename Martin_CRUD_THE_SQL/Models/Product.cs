﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martin_CRUD_THE_SQL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
    }
}
