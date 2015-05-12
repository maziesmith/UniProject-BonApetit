using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BonApetit.Models
{
    public class Recipe
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(256), Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string PrepareInstructions { get; set; }

        public virtual Image Image { get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }

        public virtual List<Category> Categories { get; set; }

        public virtual List<ApplicationUser> Users { get; set; }

        public Recipe()
        {

        }

        public Recipe(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
}