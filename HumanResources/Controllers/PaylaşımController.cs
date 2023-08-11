using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers;

public class PaylaşımController : Controller
{
    private readonly Databasecontext _databasecontext;
    public PaylaşımController(Databasecontext databasecontext)
    {
        _databasecontext = databasecontext;
    }

    [HttpGet]
    public IActionResult List()
    {
        List<paylasim> liste = _databasecontext.paylasım.OrderBy(x => x.id).ToList();
        return View(liste);
    }
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(paylasim model)
    {
        model.id = 4;
        _databasecontext.paylasım.Add(model);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }
    
    [HttpGet]
    public IActionResult Guncelle(int id)
    {
        paylasim pa = _databasecontext.paylasım.Find(id);
        return View(pa);
    }

    [HttpPost]
    public IActionResult Guncelle(paylasim model)
    {
        paylasim pa = _databasecontext.paylasım.Find(model.id);

        //kullanıcı id ve kategori id null gelir default olarak, default gelmemesini sağlaman gerekiyor....

        pa.baslik = model.baslik;
        pa.icerik = model.icerik;
        pa.olusturmatarihi = model.olusturmatarihi;

        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }
    [HttpGet]
    public IActionResult Sil(int id)
    {
        paylasim pa = _databasecontext.paylasım.Find(id);
        _databasecontext.paylasım.Remove(pa);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }




}


