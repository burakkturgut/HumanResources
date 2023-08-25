using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace HumanResources.Controllers;

public class KullaniciController : Controller
{
    private readonly Databasecontext _databasecontext;
    private logs _log; //newkedik
    public KullaniciController(Databasecontext databasecontext)
    {
        _databasecontext = databasecontext;
    }

    [HttpGet]
    public IActionResult List()
    {
        List<kullanici> liste = _databasecontext.kullanici.OrderBy(x => x.id).ToList();
        LogKullanimi("Kullanici", "list", "listeleme oldu");
        return View(liste);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(kullanici model)
    {
        model.id = 4;
        _databasecontext.kullanici.Add(model);
        _databasecontext.SaveChanges();

        LogKullanimi("Kullanici", "ekle", "ekleme oldu");
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Guncelle(int id)
    {
        kullanici za = _databasecontext.kullanici.Find(id);
        KullaniciViewModel model = new()
        {
            adi = za.adi,
            sifre = za.sifre,
            Email = za.Email,
            id = za.id,
            soyadi = za.soyadi
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Guncelle(KullaniciViewModel model, IFormFile myfile)
    {
        kullanici za = _databasecontext.kullanici.Find(model.id);

        za.adi = model.adi;
        za.soyadi = model.soyadi;
        za.sifre = model.sifre;

        string uniqueFileName = UploadedFile(model, myfile);

        za.profilfotografi = uniqueFileName;
        LogKullanimi("Kullanici", "Guncelle", "Kullanici güncellemesi oldu");
        return RedirectToAction("Profil", "Kullanici");
    }

    private string UploadedFile(KullaniciViewModel Model, IFormFile myfile_)
    {
        string uniqueFileName = null;

        if (myfile_ != null)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + myfile_.FileName; //guid dünyada benzeri olmayan karakter kümesidir
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                myfile_.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
    }

    [HttpGet]
    public IActionResult Sil(int id)
    {
        kullanici za = _databasecontext.kullanici.Find(id);
        _databasecontext.kullanici.Remove(za);
        LogKullanimi("Kullanici", "sil", "kullanici silme işlemi gerçekleşti");
        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Giriş()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Giriş(kullanici model)
    {

        if (ModelState.IsValid)
        {

            var sorgu = await _databasecontext.kullanici.FirstOrDefaultAsync(x => x.Email == model.Email && x.sifre == model.sifre);

            if (sorgu != null)
            {
                //session 
                HttpContext.Session.SetString("email", sorgu.Email);//view
                HttpContext.Session.SetString("name", sorgu.adi);//view
                HttpContext.Session.SetInt32("id", sorgu.id);//view
                HttpContext.Session.SetString("profilImage", sorgu.profilfotografi);//view
                HttpContext.Session.SetInt32("rolid", sorgu.rolid);//view

                var email = HttpContext.Session.GetString("email");
                var userid = HttpContext.Session.GetInt32("id");

                if (sorgu.rolid == 1)
                {
                    LogKullanimi("Kullanici", "Giriş", "Admin sayfasına giriş oldu");
                    return RedirectToAction("AdminPage", "Admin");
                }
                LogKullanimi("Kullanici", "Giriş", "Kullanici sayfasına giriş oldu");
                // Kullanıcı doğrulandı
                return RedirectToAction("Anasayfa", "Home"); // eğer doğruysa Home controllına gidicek anasayfa viewine gecicek
            }
            else
            {
                LogKullanimi("Kullanici", "Giriş", "Hatalı giriş denemesi");
                // Kullanıcı doğrulanamadı
                return RedirectToAction("Giris", "Home");
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult KayıtOl()
    {
        return View();
    }

    [HttpPost]
    public IActionResult KayıtOl(kullanici model)
    {

        if (ModelState.IsValid)
        {
            // Kullanıcıyı veritabanında arayın
            kullanici existingUser = _databasecontext.kullanici.FirstOrDefault(u => u.Email == model.Email);

            // Eğer kullanıcı yoksa, yeni bir kullanıcı oluşturun
            if (existingUser == null)
            {
                int userId = _databasecontext.kullanici.OrderByDescending(x => x.id).FirstOrDefault().id;

                kullanici kullanici = new()
                { 
                    adi = model.adi,
                    Email = model.Email,
                    sifre = model.sifre,
                    soyadi = model.soyadi,
                    rolid = 2,
                    profilfotografi = "/images/default.jpg"
                };
                _databasecontext.kullanici.Add(kullanici);
                LogKullanimi("Kullanici", "KayıtOl", "Yeni Kayıt oluştu");
                return RedirectToAction("Giriş", "Kullanici"); // Başka bir sayfaya yönlendirme
            }
            return RedirectToAction("Giriş", "Kullanici"); // Başka bir sayfaya yönlendirme
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Profil()
    {
        var userid = HttpContext.Session.GetInt32("id");
        var getuser = _databasecontext.kullanici.Find(userid);

        KullaniciViewModel model = new()
        {
            id = getuser.id,
            adi = getuser.adi,
            sifre = getuser.sifre,
            Email = getuser.Email,
            soyadi = getuser.soyadi,
            profilfotograf = getuser.profilfotografi
        };
        LogKullanimi("Kullanici", "Profil", "Profil ekranına gidildi");
        return View(model);
    }

    [HttpGet]
    public IActionResult KulaniciGuncelle(int id)
    {
        kullanici za = _databasecontext.kullanici.Find(id);
        KullaniciViewModel model = new()
        {
            adi = za.adi,
            sifre = za.sifre,
            Email = za.Email,
            id = za.id,
            soyadi = za.soyadi
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult KulaniciGuncelle(KullaniciViewModel model, IFormFile myfile)
    {
        kullanici za = _databasecontext.kullanici.Find(model.id);

        za.adi = model.adi;
        za.soyadi = model.soyadi;
        za.sifre = model.sifre;

        string uniqueFileName = UserUploadedFile(model, myfile);

        za.profilfotografi = uniqueFileName;
        LogKullanimi("Kullanici", "KullaniciGuncelle", "Kullanici Güncelleme Yaptı");
        return RedirectToAction("KullaniciProfil", "Kullanici");
    }

    private string UserUploadedFile(KullaniciViewModel Model, IFormFile myfile_)
    {
        string uniqueFileName = null;

        if (myfile_ != null)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + myfile_.FileName; //guid dünyada benzeri olmayan karakter kümesidir
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                myfile_.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
    }

    [HttpGet]
    public IActionResult KullaniciProfil()
    {
        var userid = HttpContext.Session.GetInt32("id");
        var getuser = _databasecontext.kullanici.Find(userid);

        KullaniciViewModel model = new()
        {
            id = getuser.id,
            adi = getuser.adi,
            sifre = getuser.sifre,
            Email = getuser.Email,
            soyadi = getuser.soyadi,
            profilfotograf = getuser.profilfotografi
        };
        LogKullanimi("Kullanici", "KullaniciProfil", "Listeleme çağrıldı");
        return View(model);
    }

    public void LogKullanimi(string kontroller, string action, string msj)
    {
        _log = new logs();
        _log.olusturmatarihi = DateTime.UtcNow.ToString();
        _log.controllername = kontroller;
        _log.actionname = action;
        _log.mesaj = msj;
        _databasecontext.logs.Add(_log);
        _databasecontext.SaveChanges();
    }
}
