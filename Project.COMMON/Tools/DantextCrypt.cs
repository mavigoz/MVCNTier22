using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Tools
{
    //Şifreleme işleme Sınıfı
    public class DantextCrypt
    {
        public static string Crypt(string a)
        {
            Random rnd=new Random();
            char[] charArray = { '*','_','?'};
            string hashedCode = "";
            foreach(char item  in a) {
                int tempInteger;
            switch (rnd.Next(1,4))
                
                {
                    case 1:
                       tempInteger = (Convert.ToInt32(item)+1)*2;
                        hashedCode += $"{tempInteger.ToString()}{charArray[0]}";
                        
                        break;
                    case 2:
                        tempInteger = (Convert.ToInt32(item) + 1) * 3;
                        hashedCode += $"{tempInteger.ToString()}{charArray[1]}";

                        break; ;
                    case 3:
                        tempInteger = (Convert.ToInt32(item) + 1) * 4;
                        hashedCode += $"{tempInteger.ToString()}{charArray[2]}";

                        break;
                    case 4:
                        tempInteger = (Convert.ToInt32(item) + 1) * 5;
                        hashedCode += $"{tempInteger.ToString()}{charArray[3]}";

                        break;
                }
            
            
            
            
            }


            return hashedCode;
        }


    }
}
