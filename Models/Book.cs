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
        // ЧАСТЬ I: DisplayName у всех свойств
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

        // ЧАСТЬ I: DataType(DataType.PhoneNumber) - вариант 2
        [DisplayName("ISBN")]
        [DataType(DataType.PhoneNumber)]
        public string ISBN { get; set; }

        [DisplayName("Цена (руб.)")]
        public decimal Price { get; set; }
    }
}