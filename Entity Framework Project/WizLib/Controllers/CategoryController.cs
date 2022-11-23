using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Migrations;
using Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProj.Controllers
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
            // "AsNoTracking" is faster than omitting it, which tracks all the changes in the database... so, for readonly purposes, we can use it to speed up things
            List<Category> objList = _db.Categories.AsNoTracking().ToList();
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            Category obj = new Category();
            if (id == null)
                return View(obj);

            obj = _db.Categories.FirstOrDefault(u => u.Category_Id == id);

            if (obj == null)
                return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Category_Id == 0)
                    _db.Categories.Add(obj);
                else
                    _db.Categories.Update(obj);

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Categories.FirstOrDefault(u => u.Category_Id == id);
            _db.Categories.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Bulk operations
        public IActionResult CreateMultiple2()
        {
            // Adding multiple records to the database
            List<Category> catList = new List<Category>();
            for (int i = 1; i <= 2; i++)
            {
                // We're adding an item of type "Category", which requires only the name
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });

                // Another method for doing it, without using Sa list
                //_db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            // Adds the list entities in the database
            _db.Categories.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // If bulk operations are less than 4, then it will execute the operations 1 by 1 because it's not optimized for operations less that are less or equal than 4
        public IActionResult CreateMultiple5()
        {
            List<Category> catList = new List<Category>();
            for (int i = 1; i <= 5; i++)
            {
                catList.Add(new Category { Name = Guid.NewGuid().ToString() });
                //_db.Categories.Add(new Category { Name = Guid.NewGuid().ToString() });
            }
            _db.Categories.AddRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple2()
        {
            // Removing the last 2 records of the database
            // Gets the last 2 records inserted in the database, ordering them, which have the greater "Category_Id", and takes 2, and put them in a list
            IEnumerable<Category> catList = _db.Categories.OrderByDescending(u => u.Category_Id).Take(2).ToList();

            // List that we remove here
            _db.Categories.RemoveRange(catList);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveMultiple5()
        {
            IEnumerable<Category> catList = _db.Categories.OrderByDescending(u => u.Category_Id).Take(5).ToList();
            _db.Categories.RemoveRange(catList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}