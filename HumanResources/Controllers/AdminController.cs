using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers
{
    public class AdminController : Controller
    {
        private readonly Databasecontext _databasecontext;

        public AdminController(Databasecontext databasecontext)
        {
            _databasecontext = databasecontext;
        }

        public IActionResult AdminPage() //buradaki isimle viewin aynı isimde olması gerekir
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