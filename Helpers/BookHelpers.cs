using PublishingHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PublishingHouse.Helpers
{
    // ЧАСТЬ III: ВНЕШНИЙ вспомогательный метод
    public static class BookHelpers
    {
        // Внешний вспомогательный метод
        // Параметризованный: принимает List<Book> и string title
        public static MvcHtmlString ExternalBookList(this HtmlHelper helper, List<Book> books, string title = "Список книг")
        {
            var html = new StringBuilder();

            html.Append($"<div class='external-book-list' style='border: 2px solid blue; padding: 15px; margin: 10px 0; border-radius: 8px;'>");
            html.Append($"<h3 style='color: blue;'>{title}</h3>");
            html.Append("<ul>");

            foreach (var book in books)
            {
                html.Append($"<li><strong>{book.Title}</strong> — {book.Author} ({book.Year}) | Цена: {book.Price:C}</li>");
            }

            html.Append("</ul>");
            html.Append("<p><em>Вызван ВНЕШНИЙ вспомогательный метод</em></p>");
            html.Append("</div>");

            return new MvcHtmlString(html.ToString());
        }
    }
}