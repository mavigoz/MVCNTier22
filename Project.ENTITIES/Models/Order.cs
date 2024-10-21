using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public   class Order:BaseEntity
    {
        public string SheepAddress { get; set; }
        public int? UserID { get; set; }
        public int? ShipperID { get; set; }


        //sorgular kolaylaşması için burdada eklemeler yapılır gerekir se sql den ignore edilkir
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        //Relational Properties
        public virtual List<OrderDetail> OrderDetails { get; set; } 
        public virtual AppUser User { get; set; }
        public virtual Shipper Shipper { get; set; }

    }
}
