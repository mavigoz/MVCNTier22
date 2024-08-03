using PagedList;
using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminVMClasses;
using Project.MVCUI.AutentecationClasses;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 

namespace Project.MVCUI.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        // [AdminAuthentication]
        CategoryRepository _cRep;
        ProductRepository _productRep;
        public CategoryController()
        {
            _cRep = new CategoryRepository();
            _productRep = new ProductRepository();
        }
        public ActionResult CategoryList(int? id)

        {

            CategoryAdminVM vM = id == null ? new CategoryAdminVM
            {
                Categories = _cRep.GetActives()



            } : new CategoryAdminVM {

                
               Categories= _cRep.Where(x=>x.ID== id)
            
            };





            return View(vM);
           
           
        }
        public ActionResult AddCategory()
        {

            return View();
        }

        [HttpPost]
 public ActionResult AddCategory(Category category)
        {

            _cRep.Add(category);

            return RedirectToAction("CategoryList");

        }
        public ActionResult UpdateCategory(int id)
        {
            // Değiştirilecek olan Id  Temdata   sonuc ismine kaydediyorum 
            //İnspact kısmından hiddenfor ile gizlediğim Id yi Değiştirebilir
            //Sonuc olarak başka category güncellemeyi Engellemek İçin Alınmış bir önlem

            TempData["sonuc"] = id;
            CategoryAdminVM vM = new CategoryAdminVM { 
            
            
            Category=_cRep.Find(id)
            };


            return View(vM);
        
        }
        [HttpPost]  
        public ActionResult UpdateCategory(Category category)
        {  //Inspecten değiştirse bile value  farketmez  kontrol altına alındı
            category.ID = Convert.ToInt32(TempData["sonuc"]);
            _cRep.Update(category);
            return RedirectToAction("CategoryList");


        }
        public ActionResult DeleteCategory(int id)
        {

            _cRep.Delete(_cRep.Find(id));
            return RedirectToAction("CategoryList");


        }

    }
}