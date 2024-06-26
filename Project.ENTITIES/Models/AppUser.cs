﻿using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser:BaseEntity
    {
        public AppUser()
        {
            Role = UserRole.Member;
            ActivetionCode = Guid.NewGuid();
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid ActivetionCode { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public UserRole Role {  get; set; }

        //Relational Properties
        public virtual  AppUserProfile Profile { get; set; }
        
    }
}
