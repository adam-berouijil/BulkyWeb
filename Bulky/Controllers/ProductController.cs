using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class ProductController : Controller
    {


        private readonly ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Products.ToList();
            return View(objProductList);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Product? obj = _db.Products.Find(id);

            if (obj == null) return NotFound();

            return View(obj);
        }


        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            /*
            if (obj.Naam == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Naam", "De naam mag niet hetzelfde zijn als de display order");
            }

            if (obj.Naam != null && obj.Naam.ToLower() == "test")
            {
                ModelState.AddModelError("", "De naam mag niet 'Test' zijn");
            }
            */
            if (ModelState.IsValid)
            {
                _db.Products.Update(obj);
                _db.SaveChanges();
                TempData["gelukt"] = "Product updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        public IActionResult Product()
        {
            return View();
        }

        public IActionResult Remove(int id)
        {
            var product = _db.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Remove(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            TempData["gelukt"] = "product removed successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        
        
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();
                TempData["gelukt"] = "Product created successfully";
                return RedirectToAction("Index", "Product");
            }
            return View(obj); // ← obj meegeven!
        }

    }
}
