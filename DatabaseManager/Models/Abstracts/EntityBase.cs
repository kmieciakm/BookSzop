using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseManager.Models.Abstracts
{
    [Serializable]
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
