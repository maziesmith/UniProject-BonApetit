using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonApetit.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; private set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public virtual List<Recipe> Recipes { get; set; }

        public Category()
        {
            this.Id = Guid.NewGuid();
        }

        public Category(string name)
            : this()
        {
            this.Name = name;
        }
    }
}