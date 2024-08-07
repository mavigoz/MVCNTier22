﻿using Project.BLL.DesingPattern.GenericRepository.ConctRep;
using Project.BLL.DesingPattern.SingletonPatterns;
using Project.ENTITIES.Models;
using Project.MVCUI.Areas.Admin.AdminVMClasses;
using Project.MVCUI.Tool;
using Project.MVCUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Areas.Admin.Controllers
{
    
    public class ProductController : Controller
    {
        ProductRepository _pRep;
        CategoryRepository _cRep;
        public ProductController()
        {
            _pRep = new ProductRepository() ;    
            _cRep = new CategoryRepository() ;
        }
        // GET: Admin/Product
        public ActionResult ProductList(int? id)
        {
            ProductAdminVM vM = id == null ? new ProductAdminVM { Products = _pRep.GetAll() } : new ProductAdminVM { Products = _pRep.Where(x => x.CategoryID == id) };
            return View(vM);
        }
       public ActionResult AddProduct()
        {

            ProductAdminVM vM = new ProductAdminVM
            {
                Products=_pRep.GetAll(),
                Categories = _cRep.GetAll()
            
            };
            
            return View(vM);
        
        
        
        
        }
        [HttpPost]
        public ActionResult AddProduct(Product  product,HttpPostedFileBase image,string fileName)
        {
            product.ImagePath = ImageUploader.UploadImage("/Picturs/", image, fileName);
            _pRep.Add(product);
            return RedirectToAction("ProductList");

        }
       
        public ActionResult UpdateProduct(int id)
        {
            ProductAdminVM vM = new ProductAdminVM { 
            
            Product=_pRep.Find(id),
            Categories = _cRep.GetActives()
            };
            //Güncellenecek Product Resim Orjinalini Aldım
            TempData["orjinResim"] = vM.Product.ImagePath;
            TempData["orjinID"] = vM.Product.ID ;
            return View(vM);
            //Güncellenecek Product Id Inspecten Müdahle Engellensin
           
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product product,HttpPostedFileBase image,string fileName)
        {
            product.ImagePath = ImageUploader.UploadImage("/Picturs/", image, fileName);

            //İmageUploader 2 ve 3 değerlerini yada resim dğru Şekilde yüklendyise yolunu döndürür
            //eğer sonuç 2 ve 3 ise öncekş Resim tempdata onu Bırakıyoruz
            if (product.ImagePath == "2" || product.ImagePath == "3")
            {

                product.ImagePath = TempData["orjinResim"].ToString();
            }
              product.ID = Convert.ToInt32(TempData["orjinID"]);

            _pRep.Update(product);

            
            return RedirectToAction("ProductList");
        
        }
                             
        public ActionResult DeleteProduct(int id)
        {

            _pRep.Delete(_pRep.Find(id));

            return RedirectToAction("ProductList");
        }


    }
}