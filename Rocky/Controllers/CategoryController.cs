using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            IEnumerable<Category> objList = _db.Category;

            return View(objList);
        }


        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]  //checks to see that token is valid and security isn't tampered
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
                _db.Category.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //GET - EDIT
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(Id);
            if(obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        //POST - UPDATE
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Category.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        //GET - DELETE
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var obj = _db.Category.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }


        //GET - DELETE
        [HttpPost]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Category.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Category.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
