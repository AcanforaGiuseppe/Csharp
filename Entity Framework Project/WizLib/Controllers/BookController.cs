using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Data;
using DataAccess.Migrations;
using Models.Models;
using Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProj.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Include means that the method loads all Books and related Publishers, BookAuthors and Authors
            // ThenInclude means that the "Author" is included in "BookAuthors" and not in "Books"
            List<Book> objList = _db.Books.Include(u => u.Publisher)
                                          .Include(u => u.BookAuthors)
                                          .ThenInclude(u => u.Author).ToList();


            /*          
            List<Book> objList = _db.Books.ToList();
            foreach (var obj in objList)
            {
                // Least Effecient
                obj.Publisher = _db.Publishers.FirstOrDefault(u => u.Publisher_Id == obj.Publisher_Id);
            	
                // Explicit Loading -> More Efficient
                _db.Entry(obj).Reference(u => u.Publisher).Load();      // Reference is used if we got one entity to show            -> 1 to 1 relation
                _db.Entry(obj).Collection(u => u.BookAuthors).Load();   // Collection is used if we got many entities to show        -> 1 to N relation

                foreach (var bookAuth in obj.BookAuthors)
                {
                    // Entry gives the access to the info on the entities and let execute actions
                    // Load loads the entity/entities in the database
                    _db.Entry(bookAuth).Reference(u => u.Author).Load();
                }
            }
            */
            return View(objList);
        }

        public IActionResult Upsert(int? id)
        {
            BookVM obj = new BookVM();
            // Projection is the technique of mapping one set of properties to another.
            // In relation to Entity Framework specifically, it’s a way of translating a full entity(database table) into a C# class with a subset of those properties.
            // The values can also be altered/joined/removed on the way through as well. Anything you can do in a SELECT statement in SQL is possible.
            obj.PublisherList = _db.Publishers.Select(i => new SelectListItem
            {
                // Here, we're mapping the Publishers into the obj.PublisherList, using the "Select" for taking the "Name" and the "Publisher_Id" from the object
                Text = i.Name,
                Value = i.Publisher_Id.ToString()
            });

            if (id == null)
                return View(obj);

            obj.Book = _db.Books.FirstOrDefault(u => u.Book_Id == id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM obj)
        {
            if (obj.Book.Book_Id == 0)
                _db.Books.Add(obj.Book);
            else
                _db.Books.Update(obj.Book);

            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            BookVM obj = new BookVM();

            if (id == null)
                return View(obj);

            obj.Book = _db.Books.Include(u => u.BookDetail).FirstOrDefault(u => u.Book_Id == id);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        // Gives the details of a Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(BookVM obj)
        {
            // If the book doesn't contains details
            if (obj.Book.BookDetail.BookDetail_Id == 0)
            {
                // We create them
                _db.BookDetails.Add(obj.Book.BookDetail);
                _db.SaveChanges();
                var BookFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == obj.Book.Book_Id);
                BookFromDb.BookDetail_Id = obj.Book.BookDetail.BookDetail_Id;
                _db.SaveChanges();
            }
            else
            {
                // We update them
                _db.BookDetails.Update(obj.Book.BookDetail);
                _db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var objFromDb = _db.Books.FirstOrDefault(u => u.Book_Id == id);
            _db.Books.Remove(objFromDb);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageAuthors(int id)
        {
            BookAuthorVM obj = new BookAuthorVM
            {
                // Where is a keyword that we put when we want to insert a condition in the result that will give the line
                // In this case, we're adding the BookAuthors if the id provided is equal to the Book_Id
                BookAuthorList = _db.BookAuthors.Include(u => u.Author).Include(u => u.Book)
                                                .Where(u => u.Book_Id == id).ToList(),
                BookAuthor = new BookAuthor()
                {
                    Book_Id = id
                },
                Book = _db.Books.FirstOrDefault(u => u.Book_Id == id)
            };

            List<int> tempListOfAssignedAuthors = obj.BookAuthorList.Select(u => u.Author_Id).ToList();
            // "NOT IN" clause in LINQ
            // Gets all the Authors whos id is not in tempListOfAssignedAuthors
            var tempList = _db.Authors.Where(u => !tempListOfAssignedAuthors.Contains(u.Author_Id)).ToList();

            obj.AuthorList = tempList.Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Author_Id.ToString()
            });

            return View(obj);
        }

        [HttpPost]
        public IActionResult ManageAuthors(BookAuthorVM bookAuthorVM)
        {
            if (bookAuthorVM.BookAuthor.Book_Id != 0 && bookAuthorVM.BookAuthor.Author_Id != 0)
            {
                _db.BookAuthors.Add(bookAuthorVM.BookAuthor);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookAuthorVM.BookAuthor.Book_Id });
        }

        [HttpPost]
        public IActionResult RemoveAuthors(int authorId, BookAuthorVM bookAuthorVM)
        {
            int bookId = bookAuthorVM.Book.Book_Id;
            BookAuthor bookAuthor = _db.BookAuthors.FirstOrDefault(u => u.Author_Id == authorId && u.Book_Id == bookId);
            _db.BookAuthors.Remove(bookAuthor);
            _db.SaveChanges();
            return RedirectToAction(nameof(ManageAuthors), new { @id = bookId });
        }

        public IActionResult PlayGround()
        {

            #region IEnumerable vs IQueryable
            /*
            IEnumerable filters the data on the client side, it returns all the records, then use the filters, then we get the response:
            it is useful when we are looking for data not filtered, just to get the entire list of something without filters
            Faster without condition, just for retrieving data
            */
            IEnumerable<Book> BookList1 = _db.Books;
            var FilteredBook1 = BookList1.Where(b => b.Price > 500).ToList();

            /*
            IQueryable filters the data on database side with the query, it returns all the filtered records, then you get the response:
            it is more useful when we are looking for data to filter and not to use memory by database side
            Faster with conditions, retrieving data with conditions on it
            */
            IQueryable<Book> BookList2 = _db.Books;
            var fileredBook2 = BookList2.Where(b => b.Price > 500).ToList();
            #endregion

            #region State of Entities
            // We can manually set a state of an entity: In this example, we put it in the state "Modified" -> It will update the entity also if we don't make changes to the record
            //var category = _db.Categories.FirstOrDefault();
            //_db.Entry(category).State = EntityState.Modified;
            //_db.SaveChanges(); 
            #endregion

            #region Update vs Attach
            // "Update" is used for updating all the fields of the record, if we don't change one of it's properties, it will update it anyway, spending time on it
            //var bookTemp1 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            //bookTemp1.BookDetail.NumberOfChapters = 2222;
            //bookTemp1.Price = 12345;
            //_db.Books.Update(bookTemp1);
            //_db.SaveChanges();

            // "Attach" updates the fields defined of the record, so it will update just the properties that we wrote
            //var bookTemp2 = _db.Books.Include(b => b.BookDetail).FirstOrDefault(b => b.Book_Id == 4);
            //bookTemp2.BookDetail.Weight = 3333;
            //bookTemp2.Price = 123456;
            //_db.Books.Attach(bookTemp2);
            //_db.SaveChanges(); 
            #endregion


            // Views
            var viewList = _db.BookDetailsFromViews.ToList();
            var viewList1 = _db.BookDetailsFromViews.FirstOrDefault();
            var viewList2 = _db.BookDetailsFromViews.Where(u => u.Price > 500);

            int id = 2;
            // RAW SQL - don't use the interpolation here, cause it allows SQL Injection attacks (it's without protection)
            var bookRaw = _db.Books.FromSqlRaw("Select * from dbo.books").ToList();
            //var bookRawWRONG = _db.Books.FromSqlRaw($"Select * from dbo.books where Book_Id={id}").ToList();

            // SQL Injection attack safe
            var bookTemp1 = _db.Books.FromSqlInterpolated($"Select * from dbo.books where Book_Id={id}").ToList();
            var booksStoreProcedure = _db.Books.FromSqlInterpolated($" EXEC dbo.getAllBookDetails {id}").ToList();

            // Conditions allowed on .NET 5 only
            var BookFilter1 = _db.Books.Include(e => e.BookAuthors.Where(p => p.Author_Id == 5)).ToList();
            var BookFilter2 = _db.Books.Include(e => e.BookAuthors.OrderByDescending(p => p.Author_Id).Take(2)).ToList();

            return RedirectToAction(nameof(Index));
        }

    }
}