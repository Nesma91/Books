using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Books.Models;

namespace Books.ViewModels
{
    public class BooksFormViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<Category> Categories { get; set;}
    }
}