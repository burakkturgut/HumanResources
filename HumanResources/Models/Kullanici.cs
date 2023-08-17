using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResources.Models
{
    public class Kullanici
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string sifre { get; set; }
        public string Email { get; set; }
        public string ProfilFotoğrafı { get; set; }
    }
}
