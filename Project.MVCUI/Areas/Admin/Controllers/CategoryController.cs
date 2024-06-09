using PagedList;
using Project.BLL.DesingPattern.GenericRepository.ConctRep;
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
        public CategoryController()
        {
            _cRep = new CategoryRepository();
        }
        public ActionResult CategoryList(int? id)

        {

            CategoryAdminVM vM = id == null ? new CategoryAdminVM
            {
                Categories = _cRep.GetActives()



            } : new CategoryAdminVM {

                Categories = _cRep.Where(x=>x.ID== id)
            
            };





            return View(vM);
           
           
        }
    }
}