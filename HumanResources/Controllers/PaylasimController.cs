using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Controllers;

public class PaylasimController : Controller
{
    private readonly Databasecontext _databasecontext;
    public PaylasimController(Databasecontext databasecontext)
    {
        _databasecontext = databasecontext;
    }

    [HttpGet]
    public IActionResult List()
    {
        List<paylasim> liste = _databasecontext.paylasim.OrderBy(x => x.id).ToList();
        return View(liste);
    }
    [HttpGet]
    public IActionResult Ekle()
    {
        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            return View();

        }
        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult Ekle(paylasim model)
    {
        model.olusturmatarihi = DateTime.Now.ToString("dd/MM/yyyy");
        _databasecontext.paylasim.Add(model);
        _databasecontext.SaveChanges();
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Guncelle(int id)
    {

        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            paylasim pa = _databasecontext.paylasim.Find(id);
            return View(pa);

        }
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult Guncelle(paylasim model)
    {
        paylasim pa = _databasecontext.paylasim.Find(model.id);

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

        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            paylasim pa = _databasecontext.paylasim.Find(id);
            _databasecontext.paylasim.Remove(pa);
            _databasecontext.SaveChanges();

            return RedirectToAction("List");

        }
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("List");

    }


    [HttpGet]
    public IActionResult KullaniciList()
    {
        var userId = Convert.ToInt32(HttpContext.Session.GetInt32("id"));

        var query = from p in _databasecontext.paylasim
                    select new BasvuruViewModel
                    {
                        icerik = p.icerik,
                        baslik = p.baslik,
                        olusturmatarihi = p.olusturmatarihi,
                        user_id = userId,
                        paylasimid = p.id
                    };

        var result = query.ToList();

        return View(result);
    }
    [HttpGet]
    public IActionResult KullaniciSil(int id)
    {
        paylasim pa = _databasecontext.paylasim.Find(id);
        _databasecontext.paylasim.Remove(pa);
        _databasecontext.SaveChanges();
        return RedirectToAction("KullaniciList");
    }

    [HttpGet]
    public IActionResult KullaniciBasvuruList(int id, int paylasimid)
    {
        var userId = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
        basvuruKontrol bk = new basvuruKontrol(); //ram de yer açtım 
        bk.durum = true;
        bk.paylasimid = paylasimid;
        bk.user_id= userId;
        _databasecontext.Add(bk);
        _databasecontext.SaveChanges();
        var query = from b in _databasecontext.basvuruKontrol
                    join k in _databasecontext.kullanici on b.user_id equals k.id
                    join p in _databasecontext.paylasim on b.paylasimid equals p.id
                    where b.durum && b.user_id == userId // Sadece kullanıcının başvurduğu ve durumu true olanları al
                    select new BasvuruViewModel
                    {
                        adi = k.adi,
                        soyadi = k.soyadi,
                        icerik = p.icerik,
                        baslik = p.baslik,
                        olusturmatarihi = p.olusturmatarihi,
                        user_id = userId
                    };

        var result = query.ToList();

        return View(result);
    }

}