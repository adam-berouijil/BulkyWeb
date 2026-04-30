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
            //custom validation
            if (obj.Naam == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Naam", "De naam mag niet hetzelfde zijn als de display order");
            }

            //custom validation
            if(obj.Naam != null && obj.Naam.ToLower() =="test") {
                ModelState.AddModelError("", "De naam mag niet 'Test' zijn");
            }

            //model validation      
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index", "Category");
            }
            return View();

    



}

    }
}
