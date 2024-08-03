using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Areas.Admin.AdminVMClasses
{
    public class OrderAdminVM
    {
        public Order Order { get; set; }
        public List<Order> Orders { get; set; }
        public Shipper Shipper {  get; set; }
        public List<Shipper> Shippers { get; set; }
    }
}