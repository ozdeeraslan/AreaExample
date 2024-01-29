using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AreaCarouselOrnek.Data;
using AreaCarouselOrnek.Areas.Admin.Models;

namespace AreaCarouselOrnek.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlaytlarController : Controller
    {
        private readonly UygulamaDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlaytlarController(UygulamaDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Slaytlar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slaytlar.ToListAsync());
        }

        // GET: Admin/Slaytlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slayt = await _context.Slaytlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slayt == null)
            {
                return NotFound();
            }

            return View(slayt);
        }

        // GET: Admin/Slaytlar/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Baslik,Aciklama,Sira,Resim")] YeniSlaytViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // Resim dosyasını yükleme işlemi
                if (vm.Resim != null)
                {
                    // Orijinal dosya adını kullanarak benzersiz bir isim oluştur
                    string ext = Path.GetExtension(vm.Resim.FileName);
                    string yeniDosyaAd = Path.GetFileNameWithoutExtension(vm.Resim.FileName) + ext;
                    string yol = Path.Combine(_env.WebRootPath, "img", "upload", yeniDosyaAd);

                    using (var fs = new FileStream(yol, FileMode.CreateNew))
                    {
                        vm.Resim.CopyTo(fs);
                    }

                    // Yeni slaytı veritabanına ekleme
                    var yeniSlayt = new Slayt
                    {
                        Baslik = vm.Baslik,
                        Aciklama = vm.Aciklama,
                        Sira = vm.Sira,
                        ResimYolu = yeniDosyaAd
                    };

                    _context.Slaytlar.Add(yeniSlayt);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { Sonuc = "Eklendi" });
                }
                else
                {
                    ModelState.AddModelError("Resim", "Resim yüklemek zorunludur.");
                }
            }

            return View(vm);
        }



        // GET: Admin/Slaytlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slayt = await _context.Slaytlar.FindAsync(id);
            if (slayt == null)
            {
                return NotFound();
            }

            var duzenleViewModel = new DuzenleSlaytViewModel
            {
                Baslik = slayt.Baslik,
                Aciklama = slayt.Aciklama,
                Sira = slayt.Sira,
                ResimYolu = slayt.ResimYolu
            };

            return View(duzenleViewModel);
        }

        // POST: Admin/Slaytlar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Baslik,Aciklama,Sira,ResimYolu,Resim")] DuzenleSlaytViewModel duzenleViewModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var slayt = await _context.Slaytlar.FindAsync(id);
                    if (slayt == null)
                    {
                        return NotFound();
                    }

                    // Mevcut resmin dosya yolunu koru
                    duzenleViewModel.ResimYolu = slayt.ResimYolu;

                    // Sadece değişen alanları güncelle
                    slayt.Baslik = duzenleViewModel.Baslik;
                    slayt.Aciklama = duzenleViewModel.Aciklama;
                    slayt.Sira = duzenleViewModel.Sira;

                    if (duzenleViewModel.Resim != null)
                    {
                        // Eğer yeni bir resim yüklenmişse, eski dosyayı sil ve ResimYolu ve dosyayı güncelle
                        var eskiResimYolu = slayt.ResimYolu;
                        string ext = Path.GetExtension(duzenleViewModel.Resim.FileName);
                        string yeniDosyaAd = Path.GetFileNameWithoutExtension(duzenleViewModel.Resim.FileName) + ext;
                        string yeniYol = Path.Combine(_env.WebRootPath, "img", "upload", yeniDosyaAd);

                        using (var fs = new FileStream(yeniYol, FileMode.CreateNew))
                        {
                            duzenleViewModel.Resim.CopyTo(fs);
                        }

                        slayt.ResimYolu = yeniDosyaAd;

                        // Eski resmi sil
                        SilinenResmiSil(eskiResimYolu);
                    }

                    _context.Update(slayt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlaytExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(duzenleViewModel);
        }


        // GET: Admin/Slaytlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slayt = await _context.Slaytlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slayt == null)
            {
                return NotFound();
            }

            return View(slayt);
        }

        // POST: Admin/Slaytlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slayt = await _context.Slaytlar.FindAsync(id);
            if (slayt == null)
            {
                return NotFound();
            }

            // Resim dosyasını silme
            SilinenResmiSil(slayt.ResimYolu);

            _context.Slaytlar.Remove(slayt);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private void SilinenResmiSil(string resimYolu)
        {
            if (!string.IsNullOrEmpty(resimYolu))
            {
                string imagePath = Path.Combine(_env.WebRootPath, "img", "upload", resimYolu);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
        }

        private bool SlaytExists(int id)
        {
            return _context.Slaytlar.Any(e => e.Id == id);
        }
    }
}
