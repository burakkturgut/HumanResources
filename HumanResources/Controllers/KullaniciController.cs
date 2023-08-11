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
        return View(za);

    }

    [HttpPost]
    public IActionResult Guncelle(Kullanici model)
    {
        Kullanici za = _databasecontext.kullanıcı.Find(model.id);

        za.adi = model.adi;
        za.soyadi = model.soyadi;
        za.sifre = model.sifre;

        _databasecontext.SaveChanges();

        return RedirectToAction("List");
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
        //kulllanıcı tablosuna sütun oluştuma 
        //linque ile kullanıcı tablosundaki verileri kontrol etme


        var sorgu = await _databasecontext.kullanıcı.FirstOrDefaultAsync(x => x.Email == model.Email && x.sifre == model.sifre);

        if (sorgu != null)
        {

            //session 
            HttpContext.Session.SetString("email", model.Email);//view
            HttpContext.Session.SetInt32("userid", model.id);//view



            // Kullanıcı doğrulandı
            return RedirectToAction("Anasayfa", "Home"); // eğer doğruysa Home controllına gidicek anasayfa viewine gecicek
        }
        else
        {
            // Kullanıcı doğrulanamadı
            return RedirectToAction("Giris", "Home");
        }


        //ssesion projeye ekleme -> kullanıcı girşi ile kullanıcının verisini tutuyosun / kullanıcı girş yapınca tüm kullanıcı bilglileri gözüksün kendi bilgileri gözüksün   1 gün
        //return RedirectToAction("List");





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

        //Kullanici existingUser = _databasecontext.kullanıcı.Find(model.Email);

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

            _databasecontext.kullanıcı.Add(kullanici);

            // Değişiklikleri kaydet
            _databasecontext.SaveChanges();

            return RedirectToAction("Giriş", "Kullanici"); // Başka bir sayfaya yönlendirme
        }
        return View(model);
    }
}
