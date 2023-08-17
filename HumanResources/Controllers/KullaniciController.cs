using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace HumanResources.Controllers;

public class KullaniciController : Controller
{
    private readonly Databasecontext _databasecontext;
    public KullaniciController(Databasecontext databasecontext)
    {
        _databasecontext = databasecontext;
    }

    [HttpGet]
    public IActionResult List()
    {
        List<Kullanici> liste = _databasecontext.kullanıcı.OrderBy(x => x.id).ToList();
        return View(liste);
    }

    [HttpGet]
    public IActionResult Ekle()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(Kullanici model)
    {
        model.id = 4;
        _databasecontext.kullanıcı.Add(model);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Guncelle(int id)
    {
        Kullanici za = _databasecontext.kullanıcı.Find(id);
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
        Kullanici za = _databasecontext.kullanıcı.Find(model.id);

        za.adi = model.adi;
        za.soyadi = model.soyadi;
        za.sifre = model.sifre;

        string uniqueFileName = UploadedFile(model, myfile);

        za.ProfilFotoğrafı = uniqueFileName;

        _databasecontext.SaveChanges();

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
        Kullanici za = _databasecontext.kullanıcı.Find(id);
        _databasecontext.kullanıcı.Remove(za);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Giriş()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Giriş(Kullanici model)
    {

        var sorgu = await _databasecontext.kullanıcı.FirstOrDefaultAsync(x => x.Email == model.Email && x.sifre == model.sifre);


        if (sorgu != null)
        {

            //session 
            HttpContext.Session.SetString("email", sorgu.Email);//view
            HttpContext.Session.SetString("name", sorgu.adi);//view
            HttpContext.Session.SetInt32("id", sorgu.id);//view
            HttpContext.Session.SetString("profilImage", sorgu.ProfilFotoğrafı);//view

            var email = HttpContext.Session.GetString("email");
            var userid = HttpContext.Session.GetInt32("id");


            if (userid == 1)
            {
                return RedirectToAction("AdminPage", "Admin");
            }

            // Kullanıcı doğrulandı
            return RedirectToAction("Anasayfa", "Home"); // eğer doğruysa Home controllına gidicek anasayfa viewine gecicek
        }
        else
        {
            // Kullanıcı doğrulanamadı
            return RedirectToAction("Giris", "Home");
        }
    }


    [HttpGet]
    public IActionResult KayıtOl()
    {
        return View();
    }

    [HttpPost]
    public IActionResult KayıtOl(Kullanici model)
    {


        // Kullanıcıyı veritabanında arayın
        Kullanici existingUser = _databasecontext.kullanıcı.FirstOrDefault(u => u.Email == model.Email);

        // Eğer kullanıcı yoksa, yeni bir kullanıcı oluşturun
        if (existingUser == null)
        {
            int userId = _databasecontext.kullanıcı.OrderByDescending(x => x.id).FirstOrDefault().id;
            userId += 1;

            Kullanici kullanici = new()
            {
                id = userId,
                adi = model.adi,
                Email = model.Email,
                sifre = model.sifre,
                soyadi = model.soyadi
            };
            //kullanici.ProfilFotoğrafı= 

            _databasecontext.kullanıcı.Add(kullanici);

            // Değişiklikleri kaydet
            _databasecontext.SaveChanges();

            return RedirectToAction("Giriş", "Kullanici"); // Başka bir sayfaya yönlendirme
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Profil()
    {       
        var userid = HttpContext.Session.GetInt32("id");
        var getuser = _databasecontext.kullanıcı.Find(userid);

        KullaniciViewModel model = new()
        {
            id = getuser.id,
            adi = getuser.adi,
            sifre = getuser.sifre,
            Email = getuser.Email,
            soyadi = getuser.soyadi,
            ProfilFotoğraf = getuser.ProfilFotoğrafı
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult KulaniciGuncelle(int id)
    {
        Kullanici za = _databasecontext.kullanıcı.Find(id);
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
        Kullanici za = _databasecontext.kullanıcı.Find(model.id);

        za.adi = model.adi;
        za.soyadi = model.soyadi;
        za.sifre = model.sifre;

        string uniqueFileName = UserUploadedFile(model, myfile);

        za.ProfilFotoğrafı = uniqueFileName;

        _databasecontext.SaveChanges();

        return RedirectToAction("Profil", "Kullanici");
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
        var getuser = _databasecontext.kullanıcı.Find(userid);

        KullaniciViewModel model = new()
        {
            id = getuser.id,
            adi = getuser.adi,
            sifre = getuser.sifre,
            Email = getuser.Email,
            soyadi = getuser.soyadi,
            ProfilFotoğraf = getuser.ProfilFotoğrafı
        };
        return View(model);
    }


}
