using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonApetit.Models
{
    public class Ingredient
    {
        [Key]
        public Guid Id { get; private set; }

        [MaxLength(256), Required]
        public string Content { get; set; }

        public Ingredient()
        {
            this.Id = Guid.NewGuid();
        }

        public Ingredient(string content)
            : this()
        {
            this.Content = content;
        }

        public static explicit operator Ingredient(string s)
        {
            Ingredient ingredient = new Ingredient(s);
            return ingredient;
        }
    }
}