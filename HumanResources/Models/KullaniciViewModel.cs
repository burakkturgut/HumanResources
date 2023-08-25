using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResources.Models
{
    public class KullaniciViewModel
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string sifre { get; set; }
        public string Email { get; set; }
        public string profilfotograf { get; set; }
        public IFormFile profilfotografi { get; set; }

    }
}
