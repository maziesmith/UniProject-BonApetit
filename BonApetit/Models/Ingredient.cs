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
        }

        public Ingredient(string content)
        {
            this.Id = Guid.NewGuid();
            this.Content = content;
        }

        public static explicit operator Ingredient(string s)
        {
            Ingredient ingredient = new Ingredient(s);
            return ingredient;
        }

        //public static implicit operator Ingredient(string s)
        //{
        //    Ingredient ingredient = new Ingredient(s);
        //    return ingredient;
        //}
    }
}