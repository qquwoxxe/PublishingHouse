using PublishingHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublishingHouse.Controllers
{
    public class BookController : Controller
    {
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;

        // GET: Book/Index - список всех книг
        public ActionResult Index()
        {
            return View(_books);
        }

        // GET: Book/Select/{id} - выбор текущей книги (сохранение в Session)
        public ActionResult Select(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                // Сохраняем ID выбранной книги в Session
                Session["CurrentBookId"] = id;
                TempData["Success"] = $"Выбрана книга: {book.Title} ({book.Author})";
            }
            return RedirectToAction("Index");
        }

        // GET: Book/Details/{id} - просмотр одной книги
        public ActionResult Details(int? id)
        {
            // Если id не передан, используем текущую книгу из Session
            if (id == null)
            {
                int? currentId = Session["CurrentBookId"] as int?;
                if (currentId.HasValue && _books.Any(b => b.Id == currentId.Value))
                {
                    return RedirectToAction("Details", new { id = currentId.Value });
                }

                if (_books.Any())
                {
                    return RedirectToAction("Details", new { id = _books.First().Id });
                }
                return RedirectToAction("Index");
            }

            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Обновляем текущую книгу в Session при просмотре
            Session["CurrentBookId"] = id;

            return View(book);
        }

        // GET: Book/Create - форма добавления
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create - сохранение новой книги
        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = _nextId++;
                _books.Add(book);
                TempData["Success"] = $"Книга \"{book.Title}\" успешно добавлена!";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book/Edit/{id} - форма редактирования
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                int? currentId = Session["CurrentBookId"] as int?;
                if (currentId.HasValue)
                {
                    return RedirectToAction("Edit", new { id = currentId.Value });
                }
                return RedirectToAction("Index");
            }

            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Edit - сохранение изменений
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var existing = _books.FirstOrDefault(b => b.Id == book.Id);
                if (existing != null)
                {
                    existing.Title = book.Title;
                    existing.Author = book.Author;
                    existing.Genre = book.Genre;
                    existing.Year = book.Year;
                    existing.PageCount = book.PageCount;
                    existing.ISBN = book.ISBN;
                    existing.Price = book.Price;
                    TempData["Success"] = $"Книга \"{book.Title}\" успешно обновлена!";
                }
                return RedirectToAction("Index");
            }
            return View(book);
        }
    }
}