﻿using HumanResources.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers;

public class KategoriController : Controller
{
    // katmalar arasında bağımlılıkları azaltmak için kullnılır, new leme yapmıyoruz
    private readonly Databasecontext _databasecontext;

    public KategoriController(Databasecontext databasecontext)
    {
        _databasecontext = databasecontext;
    }
    [HttpGet]
    public IActionResult List()
    {
        List<kategori> liste = _databasecontext.kategori.OrderBy(x => x.id).ToList();
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
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult Ekle(kategori model)
    {
        model.id = 4;
        _databasecontext.kategori.Add(model);
        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Guncelle(int id)
    {
        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            kategori ka = _databasecontext.kategori.Find(id);
            return View(ka);
        }
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("List");
    }

    [HttpPost]
    public IActionResult Guncelle(kategori model)
    {
        kategori ka = _databasecontext.kategori.Find(model.id);

        ka.kategoriadi = model.kategoriadi;

        _databasecontext.SaveChanges();

        return RedirectToAction("List");
    }

    [HttpGet]
    public IActionResult Sil(int id)
    {
        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            kategori ka = _databasecontext.kategori.Find(id);
            _databasecontext.kategori.Remove(ka);
            _databasecontext.SaveChanges();
        }
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("List");

    }
    public IActionResult KullaniciList()
    {
        List<kategori> liste = _databasecontext.kategori.OrderBy(x => x.id).ToList();
        return View(liste);
    }

    [HttpGet]
    public IActionResult KullaniciEkle()
    {
        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            return View();

        }
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("KullaniciList");
    }


    [HttpPost]
    public IActionResult KullaniciEkle(kategori model)
    {
        model.id = 4;
        _databasecontext.kategori.Add(model);
        _databasecontext.SaveChanges();

        return RedirectToAction("KullaniciList");
    }

    [HttpGet]
    public IActionResult KullaniciGuncelle(int id)
    {
        var rolid = HttpContext.Session.GetInt32("rolid");
        if (rolid == 1)
        {
            kategori ka = _databasecontext.kategori.Find(id);
            return View(ka);
        }
        TempData["RolidMesaj"] = "Yetkiniz yoktur";
        return RedirectToAction("KullaniciList");
    }

    [HttpPost]
    public IActionResult KullaniciGuncelle(kategori model)
    {
        kategori ka = _databasecontext.kategori.Find(model.id);

        ka.kategoriadi = model.kategoriadi;

        _databasecontext.SaveChanges();

        return RedirectToAction("KullaniciList");
    }

    [HttpGet]
    public IActionResult KullaniciSil(int id)
    {
        kategori ka = _databasecontext.kategori.Find(id);
        _databasecontext.kategori.Remove(ka);
        _databasecontext.SaveChanges();
        return RedirectToAction("KullaniciList");
    }
}
