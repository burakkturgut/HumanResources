namespace HumanResources.Models
{
    public class BasvuruViewModel
    {
        public int paylasimid { get; set; }
        public int user_id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string baslik { get; set; }
        public string icerik { get; set; }
        public string olusturmatarihi{ get; set; }
        public bool durum { get; set; }
    }
}
