using Microsoft.AspNetCore.Mvc;
using TeknikServisTanitim.DAL;

namespace TeknikServisTanitim.Controllers
{
    public class RequestController : Controller
    {
        private readonly TeknikServisDbContext _context;

        public RequestController(TeknikServisDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DemoRequest model)
        {
            if (ModelState.IsValid)
            {
                _context.DemoRequests.Add(model);
                _context.SaveChanges();
                TempData["AdSoyad"] = model.AdSoyad;
                return RedirectToAction("Confirmation");
            }
            return View(model);
        }

        public IActionResult Confirmation()
        {
            return View();
        }

        public IActionResult List(string search, int page = 1, int pageSize = 10)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login", "Admin");
            }

            var talepler = _context.DemoRequests.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                talepler = talepler.Where(r =>
                    r.AdSoyad.Contains(search) ||
                    r.Eposta.Contains(search));
            }

            ViewBag.YeniSayisi = _context.DemoRequests.Count(r => r.Durum == "Yeni");
            ViewBag.OkunduSayisi = _context.DemoRequests.Count(r => r.Durum == "Okundu");
            ViewBag.TamamlandiSayisi = _context.DemoRequests.Count(r => r.Durum == "Tamamlandı");

            var totalCount = talepler.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var liste = talepler
                .OrderByDescending(r => r.Tarih)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;

            return View(liste);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login", "Admin");
            }

            var talep = _context.DemoRequests.Find(id);
            if (talep != null)
            {
                _context.DemoRequests.Remove(talep);
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult ChangeStatus(int id, string newStatus)
        {
            var isAdmin = HttpContext.Session.GetString("IsAdmin");
            if (isAdmin != "true")
            {
                return RedirectToAction("Login", "Admin");
            }

            var talep = _context.DemoRequests.Find(id);
            if (talep != null)
            {
                talep.Durum = newStatus;
                _context.SaveChanges();
            }

            return RedirectToAction("List");
        }
    }
}
