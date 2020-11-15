using DatabaseManager.Models.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseManager.Models
{
    [Serializable]
    public class Book : EntityBase
    {
        [MaxLength(128)]
        public string Title { get; set; }
        [MaxLength(128)]
        public string Author { get; set; }

        public ICollection<BookBundle> BookBundles { get; set; }
    }
}
