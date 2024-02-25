using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Books.Models;
using Books.ViewModels;

namespace Books.Controllers
{
    public class BooksController : Controller
    {
        private AppDbContext _context;
        public BooksController()
        {
            _context = new AppDbContext();
        }
        // GET: Book
        public ActionResult Index()
        {
            var books = _context.Books.Include(b => b.Category).ToList();

            return View(books);
        }

        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var book = _context.Books.Include(b => b.Category).SingleOrDefault(b => b.Id == id);

            if(book == null)
            {
                return new HttpNotFoundResult();
            }
            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);

            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if(bookInDb == null)
            {
                return new HttpNotFoundResult();
            }

            var viewModel = new BooksFormViewModel
            {
                Book = bookInDb,
                Categories = _context.Categories.ToList()
            };

            return View("Create", viewModel);
        }
        public ActionResult Create() 
        {
            var viewModel = new BooksFormViewModel
            {
                Book = new Book(),
                Categories = _context.Categories.Where(c => c.IsActive).ToList()
            };
            
                return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BooksFormViewModel bookViewModel)
        {
            if(!ModelState.IsValid && bookViewModel.Book.Id != 0) //not valid // problem in id
            {
                bookViewModel.Categories = _context.Categories.Where(c => c.IsActive).ToList();

                return View("Create", bookViewModel);
            }

            if(bookViewModel.Book.Id == 0)
            {
                var book = new Book
                {
                    Title = bookViewModel.Book.Title,
                    Author = bookViewModel.Book.Author,
                    CategoryId = bookViewModel.Book.CategoryId,
                    Description = bookViewModel.Book.Description
                };

                _context.Books.Add(book);
            }
            else
            {
                var bookInDb = _context.Books.SingleOrDefault(b => b.Id ==  bookViewModel.Book.Id);
                if(bookInDb == null)
                {
                    return HttpNotFound();
                }
                bookInDb.Title = bookViewModel.Book.Title;
                bookInDb.Author = bookViewModel.Book.Author;
                bookInDb.CategoryId = bookViewModel.Book.CategoryId;
                bookInDb.Description = bookViewModel.Book.Description;
            }

            TempData["Message"] = "Saved Successfully";
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}