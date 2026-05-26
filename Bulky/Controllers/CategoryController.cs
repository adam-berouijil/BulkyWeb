using Bulky.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
       
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj) {
            
            if (obj.Naam == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Naam", "De naam mag niet hetzelfde zijn als de display order");
            }

            
            if(obj.Naam != null && obj.Naam.ToLower() =="test") {
                ModelState.AddModelError("", "De naam mag niet 'Test' zijn");
            }

                
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["gelukt"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();

    



        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? obj = _db.Categories.Find(id);

            if (obj == null) return NotFound();

            return View(obj);
        }

       
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
           
            if (obj.Naam == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Naam", "De naam mag niet hetzelfde zijn als de display order");
            }

            if (obj.Naam != null && obj.Naam.ToLower() == "test")
            {
                ModelState.AddModelError("", "De naam mag niet 'Test' zijn");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["gelukt"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj); 
        }


        public IActionResult Remove(int id)
        {
            var category = _db.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Remove(Category category)
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["gelukt"] = "Category removed successfully";
            return RedirectToAction("Index");
        }
    }
}
