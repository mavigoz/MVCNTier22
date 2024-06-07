using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    { AppUserRepository _appUSer;
        public HomeController()
        {
            _appUSer = new AppUserRepository();
        }
        // GET: Home
        public ActionResult Index()
        {


            Session["Emre"] =null;
            
            return View( );
        }
        public ActionResult Login() {
            
            return View(); }

        [HttpPost]
        public ActionResult Login(AppUser appUser)
        {
            AppUser yakalanan = _appUSer.FirstOrDefault(x => x.UserName == appUser.UserName);
            //bool sonuc=_appUSer.Any(x=>x.UserName==appUser.UserName    );
            if (yakalanan == null)
            {
                ViewBag.Kullanici = "Kullanici Bulunumadı";




                return View();
            }
           
                                                 
            string decrypted= DantextCrypt.DeCriypt(yakalanan.Password);
                if (decrypted == appUser.Password && yakalanan.Role==ENTITIES.Enums.UserRole.Admin)
                {

                if (!yakalanan.Active )
               
                    return AktifKontrol();
                Session["admin"] = yakalanan;
                return RedirectToAction("CategoryList", "Category", new { area = "Admin" });
                
                
                
                
              } else if(yakalanan.Role==ENTITIES.Enums.UserRole.Member && appUser.Password==decrypted )
            {
                if(!yakalanan.Active ) return AktifKontrol();
                Session["member"] = yakalanan;
                return RedirectToAction("ShoppingList", "Shopping");

            }

            ViewBag.Kullanici = "Kullanici Bulunaadı";
            return View();
 

        }

        private ActionResult AktifKontrol()
        {
            TempData["Hesap Aktifmi"] = "Lütfen hesabını aktif hale getiriniz";
            return View("Login");
        }

        public ActionResult Deneme()
        {  return View(); }
    }
}