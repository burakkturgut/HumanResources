using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers;

public class KullaniciDetayController : Controller
{
    // katmalar arasında bağımlılıkları azaltmak için kullnılır, new leme yapmıyoruz
    private readonly Databasecontext _databasecontext;

    public KullaniciDetayController(Databasecontext databasecontext)
    {
        _databasecontext = databasecontext;
    }

    [HttpGet]
    public IActionResult List()
    {
        List<kullanicidetay> liste = _databasecontext.kullanicidetay.OrderBy(x => x.id).ToList();
        return View(liste);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(kullanicidetay model)
    {
        _databasecontext.kullanicidetay.Add(model);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Guncelle(int id)
    {
        kullanicidetay da = _databasecontext.kullanicidetay.Find(id);
        return View(da);
    }

    [HttpPost]
    public IActionResult Guncelle(kullanicidetay model)
    {
        kullanicidetay da = _databasecontext.kullanicidetay.Find(model.id);

        da.id = model.id;
        da.kullaniciid = model.kullaniciid;
        da.adres = model.adres;
        da.tc = model.tc;
        da.sehir = model.sehir;



        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Sil(int id)
    {
        kullanicidetay da = _databasecontext.kullanicidetay.Find(id);
        _databasecontext.kullanicidetay.Remove(da);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }
}
