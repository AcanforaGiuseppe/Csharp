using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Migrations;
using Models.Models;

namespace EntityFrameworkProj.Controllers
{
    // We make this views (controllers of html's pages) childs of Controller
    public class PublisherController : Controller
    {
        // Granting our access in readonly to the database
        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }

        // The action in which we write the methods are "IActionResult" types
        public IActionResult Index()
        {
            // We get a list of publishers in a list
            List<Publisher> objList = _db.Publishers.ToList();
            // and returning it in the view
            return View(objList);
        }

        // Inserting or updating a publisher
        // Upsert -> mix of "Update" and "Insert"
        public IActionResult Upsert(int? id)
        // "int?" is nullable, that means that if we're inserting something, we'll not retrieve an id and we don't need it, but if we're updating something, we'll retrieve the id
        {
            // Creates a new obj "Publisher"
            Publisher obj = new Publisher();

            // If the id doesn't exists, so we insert the record
            if (id == null)
                return View(obj);

            // Here, we update the record
            // When we want to retrieve all the list of objs, we use .ToList();
            // Instead, if we want just one, we do First(), but it will throw an exception if the file doesn't exists.
            // So, we use FirstOrDefault for handle the exception case, which returns the first record found in the database, with the condition that it will have that id
            obj = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id);

            // If obj doesn't exists, we haven't found that record with the provided id
            if (obj == null)
                return NotFound();

            return View(obj);
        }

        // Inserting records in the database from HTTP needs the specific "[HttpPost]"
        // Allows writing on the db, if the action is to "post"
        [HttpPost]
        [ValidateAntiForgeryToken] // To maintain the security
        public IActionResult Upsert(Publisher obj)
        {
            // If the obj that we're trying to add is valid
            if (ModelState.IsValid)
            {
                if (obj.Publisher_Id == 0)
                    // Creates the obj
                    _db.Publishers.Add(obj);
                else
                    // Updates the obj
                    _db.Publishers.Update(obj);

                // We need to save changes to the db after every action that we do with it
                _db.SaveChanges();
                // Redirects to action of the method "Index" -> it will retrieve the list of objs
                return RedirectToAction(nameof(Index));
            }
            return View(obj);

        }

        // If an id is provided, this method deletes the record of the following id
        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == id);
            // Removes the record of the obj provided with the condition on the last row
            _db.Publishers.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}