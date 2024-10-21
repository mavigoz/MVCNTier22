using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        AppUserRepository _apRep;
        AppUserProfileRepository _proRep;
        public RegisterController()
        {
            _apRep = new AppUserRepository();
            _proRep = new AppUserProfileRepository();
        }
        public ActionResult RegisterNow()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterNow(AppUser appUser, AppUserProfile profile)
        {
            appUser.Password = DantextCrypt.Crypt(appUser.Password);
            if (_apRep.Any(x => x.UserName == appUser.UserName))
            {

                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alınmış";
                return View();
            
            } else if(_apRep.Any(x=>x.Email==appUser.Email))
            {

                ViewBag.ZatenVar = "Email Zaten Kayıtlı";
                return View();


            }
            string gonderileceEmail="Tebrikler Hesabınız Oluştu.. Hesabınınzı active Etmek için  https://localhost:44338/Register/Activation/"+appUser.ActivetionCode+" linkine tıklayabilirsiniz"; //Boşluk bırakmassan link uzar
            MailsServis.Send(appUser.Email, body: gonderileceEmail, subject: "Hesap Aktivasyon !!!");
                         
            _apRep.Add(appUser);

           // if (!string.IsNullOrEmpty(appUserProfile.FirstName.Trim()) //|| !string.IsNullOrEmpty(appUserProfile.LastName.Trim()))
           /// {
                profile.ID = appUser.ID;
                _proRep.Add(profile);
             
            
          //  }

            return View("RegisterOK");


        }
        public ActionResult Activation(Guid id)
        {
            AppUser aktivEdilecek = _apRep.FirstOrDefault(x => x.ActivetionCode == id);
            if (aktivEdilecek != null)
            {

                aktivEdilecek.Active = true;
                _apRep.Update(aktivEdilecek);
                TempData["HesapAktifmi"] = "Hesabınız Aktif"
;                return RedirectToAction("Login","Home");
            }
            else
            {
                TempData["HesapAktifmi"] = "Hesap Bulunamadı";
                //Başka database şüpheli olan database logla
                return RedirectToAction("Login","Home");
            }
       
        }

        public ActionResult RegisterOK()
        {


            return View();
        }

        
    }
}