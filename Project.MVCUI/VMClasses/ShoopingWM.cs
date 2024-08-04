using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class ShoopingWM
    {
     
        public List<CartItem> CartItems { get; set; }
      public  Category Category { get; set; }
        public Cart Carts { get; set; }
    }
}