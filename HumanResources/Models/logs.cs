namespace HumanResources.Models
{
    public class logs
    {
        public int id { get; set; }
        public string controllername { get; set; }
        public string actionname { get; set; }
        public string mesaj { get; set; }
        public string olusturmatarihi { get; set; }
        public int kullaniciid { get; set; }

    }
}
