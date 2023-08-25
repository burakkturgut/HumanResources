using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResources.Models
{
    public class kullanici
    {
        public int id { get; set; }
        public string? adi { get; set; }
        public string? soyadi { get; set; }
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [DataType(DataType.Password)]
        public string sifre { get; set; }
        [Required(ErrorMessage = "{0} alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }
        public string? profilfotografi { get; set; }
        public int rolid { get; set; }
    }
}
