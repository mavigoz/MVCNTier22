﻿using Project.ENTITIES.Models;
using Project.MVCUI.ConsumerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.MVCUI.VMClasses
{
    public class OrderVM
    {
        public Order     Order { get; set; }
        public List<Order> Orders { get; set; }
        public PaymentDTO PaymentDTO { get; set; }

        //PaganitonVm APIBanka kısmı, template Entegrasyonu,üyelik Entegrasyon Admin Crud İşlemleri Sipariş İşlemleri  (Banka İle Birlikte)


        //Orderda bankayı Halledicez
    }
}