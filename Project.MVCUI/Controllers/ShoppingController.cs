using PagedList;
using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.ConsumerDTO;
using Project.MVCUI.Models.ShoppingTools;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            else { 
            return RedirectToAction("CartPage");
            }
           
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

        public ActionResult ComfirmOrder() {

            AppUser curentUser;
            if (Session["Member"] != null)
            {


                curentUser = Session["Member"] as AppUser;


            }
            else { TempData["anonim"] = "Kullanıcı üye Değil"; }
            return View();
        
        }
        [HttpPost]          
        public ActionResult ComfirmOrder(OrderVM orderVM)
        {

            bool result;
            Cart sepet = Session["Scart"] as Cart;
        orderVM.Order.TotalPrice=orderVM.PaymentDTO.ShoopingPrice=sepet.TotalPrice;

            #region APISection
            using (HttpClient client=new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44311/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment",orderVM.PaymentDTO);
                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {
                   
                    return RedirectToAction("ShoppingList");
                }
                if (sonuc.IsSuccessStatusCode)
                    result = true;
                else   result = false;

                if (result)
                {
                    if (Session["Member"] != null)
                    {
                        AppUser appUser = Session["Member"] as AppUser;
                        orderVM.Order.UserName = appUser.UserName;
                        orderVM.Order.UserID = appUser.ID;




                    } else {

                        orderVM.Order.UserID = null;
                        orderVM.Order.UserName = TempData["anonim"].ToString();

                    }

                    _oRep.Add(orderVM.Order);
                    foreach (CartItem item in sepet.Listele)
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderID = orderVM.Order.ID;
                        od.ProductID = item.ID;
                        od.TotalPrice = item.SubTotal;
                        od.Quantity = item.Amount;
                        _odRep.Add(od);


                        //Stoktan düş


                        Product stokDus = _pRep.Find(od.ProductID);
                        stokDus.UnitsInStock -= od.Quantity;
                    }

                    TempData["odeme"] = "Siparişiniz bize Ulaşmıştır ....";
                    MailsServis.Send(orderVM.Order.Email, body:"siparis", subject: $"Siparişini {orderVM.Order.TotalPrice} TL");
 
                    return RedirectToAction("ShoppingList");
                } else {
                    TempData["sorun"] = sonuc.Content;

                    return RedirectToAction("ShoppingList");

                }

                 
            }



                #endregion


             
        }

        //https://localhost:44311/Payment/ReceivePayment
    }
}