﻿using Microsoft.AspNetCore.Identity;

namespace HumanResources.Models
{
    public class Kullanici
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string sifre { get; set; }
        public string Email { get; set; }
    }
}