﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.ConsumerDTO
{
    public class PaymentDTO
    {
        //Sanal Poz

        //Normalde Bu tarz sınıflar çalışacağınız dökümantasyonların yönlendirmesi

        public int ID { get; set; }
        public string CardUserName   { get; set; }
        public string SecurityNumber { get; set; }
        public string CardNumber { get; set; }
        public int CardExpiryMonth { get; set; }
        public int CardExpiryYear { get; set; }
        public decimal ShoopingPrice { get; set; }
                      
    }
   
}