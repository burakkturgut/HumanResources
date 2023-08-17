using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers
{
    public class HomeController : Controller
    {
        private readonly Databasecontext _databasecontext;

        public HomeController(Databasecontext databasecontext)
        {
            _databasecontext = databasecontext;
        }

        public IActionResult Anasayfa() //buradaki isimle viewin aynı isimde olması gerekir
        {
            var user = HttpContext.Session.GetString("email");
            if (user == null)
            {
                return RedirectToAction("Giriş", "Kullanici");
            }
            return View();
        }






    }

}
