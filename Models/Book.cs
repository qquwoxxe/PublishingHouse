using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PublishingHouse.Models
{
    public class Book : Controller
    {
        // GET: Books
        [DisplayName("ID книги")]
        public int Id { get; set; }

        [DisplayName("Название книги")]
        public string Title { get; set; }

        [DisplayName("Автор")]
        public string Author { get; set; }

        [DisplayName("Жанр")]
        public string Genre { get; set; }

        [DisplayName("Год издания")]
        public int Year { get; set; }

        [DisplayName("Количество страниц")]
        public int PageCount { get; set; }

        [DisplayName("ISBN")]
        [DataType(DataType.PhoneNumber)]  // Вариант 2
        public string ISBN { get; set; }

        [DisplayName("Цена (руб.)")]
        public decimal Price { get; set; }
    }
}