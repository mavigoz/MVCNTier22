using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Project.MVCUI.Tool
{
    public static class ImageUploader
    {


        public static string UploadImage(string serverPath,HttpPostedFileBase file,string name)
            //eğğer Mvc de  Post yöntemi ile bir dosya geliyorsa bu dosya ,HttpPostedFileBase tipinde tutulur
        {
            if (file != null)
            {
                Guid uniqName = Guid.NewGuid();
                string[] fileArray = file.FileName.Split('.');

                string extension = fileArray[fileArray.Length - 1].ToLower();
                string fileName = $"{uniqName}.{name}.{extension}";

                if (extension == "jpg" || extension == "gif" || extension == "png")
                {

                    if (File.Exists(HttpContext.Current.Server.MapPath(serverPath + fileName)))
                    {



                        return "1"; //.dosya var kodu  uniqId kullandığımız için sıkıntı yok

                    }
                    else
                    {
                        string filePath = HttpContext.Current.Server.MapPath(serverPath + fileName);
                        file.SaveAs(filePath);
                        return serverPath + fileName;
                    }



                }
                else
                {
                    return "2"; //Seçilen dosya Resim Değil }

                }
            }
            else { return "3"; //Dosya Boş Kodu
                               } 
                
            
        }








    }
}