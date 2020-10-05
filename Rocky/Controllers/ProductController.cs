using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _db;

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;

            return View(products);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {

                _db.Add(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        
        //GET - EDIT
        public IActionResult Edit(int? Id)
        {
            if(Id == null || Id == 0)
            {
                return NotFound();
            }
            
            var product = _db.Products.Find(Id);
            if(product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //POST - EDIT
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {

                _db.Update(product);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }


        //GET - DELETE
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            var product = _db.Products.Find(Id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        //POST - DELETE
        [HttpPost]
        public IActionResult DeletePost(int? Id)
        {
            var obj = _db.Products.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}
