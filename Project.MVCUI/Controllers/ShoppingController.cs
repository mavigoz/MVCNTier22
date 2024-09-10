using PagedList;
using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Project.MVCUI.Controllers
{
    public class ShoppingController : Controller
    {
        OrderRepository _oRep;
        ProductRepository _pRep;
        CategoryRepository _cRep;
        OrderDeatilRespository _odRep;
        public ShoppingController()
        {

            _oRep = new OrderRepository();
            _odRep=new OrderDeatilRespository();
            _pRep = new ProductRepository();
            _cRep = new CategoryRepository();

        }

        public ActionResult ShoppingList(int? page, int? categoryID)
        {
            PaginationVM pavm = new PaginationVM {


            PagedProducts = categoryID == null ? _pRep.GetActives().ToPagedList(page??1, 9) : _pRep.Where(x => x.CategoryID == categoryID).ToPagedList(page??1, 9)
                ,
                Categories=_cRep.GetActives()
            };
            if (categoryID != null)
            { TempData["catID"] = categoryID; }

            return View(pavm);
        }
        public ActionResult AddToCart(int id) {
            Cart c = Session["Scart"] == null ? new Cart() : Session["Scart"] as Cart;
            Product Pro = _pRep.Find(id);
            CartItem item1 = new CartItem { 
             ID= Pro.ID, Name= Pro.ProductName, ImagePath= Pro.ImagePath, Price= Pro.UnitPrice
            
            
            };
            c.SepeteEkle(item1);
         
            Session["Scart"] = c;
            return RedirectToAction("ShoppingList");
        }
     
        public ActionResult DeleteFromCArt(int id)
        {
            Cart c = Session["Scart"] == null ? new Cart() : Session["Scart"] as Cart;
            c.SepettenCikar(id);

            if (c.TotalPrice == 0)
            {
                Session.Remove("Scart");
                TempData["Empty"] = "Sepetteki Tüm ürünler Çıkartılmıştır";
                return RedirectToAction("ShoppingList");

            }
            else
            return RedirectToAction("CartPage");
        }
        public ActionResult CartPage()
        {
            if (Session["Scart"] != null)
            { Cart c = Session["Scart"] as Cart;
                CartPageVM vM = new CartPageVM
                {
                    Cart = c,

                    CartItems=c.Listele



                };




        return View(vM);
            }
           

            TempData["Empty"] = "sepet Boş";

            return RedirectToAction("ShoppingList");
        }
    }
}