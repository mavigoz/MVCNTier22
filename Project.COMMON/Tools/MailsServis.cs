using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
 
 

namespace Project.COMMON.Tools
{
    public class MailsServis
    {
        public static void Send(string receiver, string password = "ysvmxbfjnpfcbhhr", string body = "Test Mesejıdır", string subject = "Email Testi", string sender = "emrefidan2217@gmail.com")
        {
            




            MailAddress senderEmail = new MailAddress(sender);
            MailAddress receiverEmail = new MailAddress(receiver);

            //Bizim Email İşlemleirmizi için SMTP ye göre yapılır..
            //kullandığımız gmail hesabının başka uygulamar tarafından mesaj gönderme özelliğini açmalıısnız...
            SmtpClient smtp = new SmtpClient
            {

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)

                //dfdgfd

            };
            using (MailMessage message = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body

            })
            {
                smtp.Send(message);

            }
        }
    }
}
