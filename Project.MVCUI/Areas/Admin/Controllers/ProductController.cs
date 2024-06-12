using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.BLL.DesingPattern.SingletonPatterns;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminVMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    
    public class ProductController : Controller
    {
        ProductRepository _pRep;

        public ProductController()
        {
            _pRep = new ProductRepository() ;        
        }
        // GET: Admin/Product
        public ActionResult ProductList(int? id)
        {
            ProductAdminVM vM = id == null ? new ProductAdminVM { Products = _pRep.Where(x => x.ID == id) } : new ProductAdminVM { Products = _pRep.GetAll() };
            return View(vM);
        }
       public ActionResult AddProduct()
        { return View();
        
        
        
        
        }
        [HttpPost]
        public ActionResult AddProduc(Product  product)
        {

            _pRep.Add(product);
            return RedirectToAction("ProductList");

        }
        public ActionResult UpdateProduct()
        { return View(); }
        public ActionResult UpdateProduct(int id)
        {
            ProductAdminVM vM = new ProductAdminVM { 
            
            Product=_pRep.Find(id)
            };
        
        return View(vM);
        
        }
        public ActionResult DeleteProduct(int id)
        {

            _pRep.Delete(_pRep.Find(id));

            return RedirectToAction("ProductList");
        }


    }
}