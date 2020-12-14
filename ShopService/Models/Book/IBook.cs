using System;
using System.Collections.Generic;
using System.Text;

namespace ShopService.Models.BookModel
{
    public interface IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
