using PublishingHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PublishingHouse.Controllers
{
    public class BookController : Controller
    {
        // ==================== ЧАСТЬ II ====================
        // Хранилище книг в памяти (List<T> - вариант 2)
        private static List<Book> _books = new List<Book>();
        private static int _nextId = 1;

        // ==================== ЧАСТЬ III ====================
        // ВНУТРЕННИЙ вспомогательный метод (параметризованный)
        public MvcHtmlString InternalBookList(List<Book> books, string title = "Мои книги")
        {
            var html = new StringBuilder();

            html.Append($"<div class='internal-book-list' style='border: 2px solid green; padding: 15px; margin: 10px 0; border-radius: 8px;'>");
            html.Append($"<h3 style='color: green;'>{title}</h3>");
            html.Append("<ul>");

            foreach (var book in books)
            {
                html.Append($"<li><strong>{book.Title}</strong> — {book.Author} ({book.Year}) | {book.PageCount} стр.</li>");
            }

            html.Append("</ul>");
            html.Append("<p><em>Вызван ВНУТРЕННИЙ вспомогательный метод</em></p>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }

        // ЧАСТЬ II: просмотр всех данных
        // ЧАСТЬ III: передача логического значения через TempData (вариант 2)
        public ActionResult Index()
        {
            // ЧАСТЬ III: TempData для выбора метода
            TempData["UseInternalMethod"] = true;  // true - внутренний, false - внешний

            return View(_books);
        }

        // ЧАСТЬ II: сохранение текущего экземпляра через Session
        public ActionResult Select(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                Session["CurrentBookId"] = id;
                TempData["Success"] = $"Выбрана книга: {book.Title}";
            }
            return RedirectToAction("Index");
        }

        // ЧАСТЬ I: просмотр одной книги
        public ActionResult Details(int? id)
        {
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

            Session["CurrentBookId"] = id;
            return View(book);
        }

        // ЧАСТЬ I: форма добавления
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

        // ЧАСТЬ I: форма редактирования
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