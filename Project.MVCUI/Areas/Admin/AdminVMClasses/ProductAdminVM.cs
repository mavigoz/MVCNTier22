﻿using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Areas.Admin.AdminVMClasses
{
    public class ProductAdminVM
    {
        public Product Product { get; set; }
        public List<Product>     Products    { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }  
    }
}