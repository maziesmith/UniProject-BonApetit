using BonApetit.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Security;
using BonApetit.Models.Users;

namespace BonApetit.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            this.AddRoles(context);

            this.AddAdminUser(context);

            //this.AddRecipes(context);
        }

        private void AddRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(
                r => r.Name,
                new IdentityRole(RoleConst.Administrator),
                new IdentityRole(RoleConst.User)
            );

            context.SaveChanges();
        }

        private void AddAdminUser(ApplicationDbContext context)
        {
            var adminUserExist = context.Users.Any(u => u.UserName == UserConst.AdminUsername);
            if (!adminUserExist)
            {
                var adminUser = new ApplicationUser()
                {
                    UserName = UserConst.AdminUsername,
                    Email = UserConst.AdminMail
                };

                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                manager.Create(adminUser, UserConst.AdminPassword);

                manager.AddToRole(adminUser.Id, RoleConst.Administrator);
                manager.AddToRole(adminUser.Id, RoleConst.User);
            }
        }

        private void AddRecipes(ApplicationDbContext context)
        {
            context.Recipes.AddOrUpdate(r => r.Name,
                new Recipe("Loaded Nacho Totchos")
                {
                    PrepareInstructions = @"Preheat oven to 375 degrees F. Bake Tater Tots according to directions on a large baking sheet.
                        Meanwhile, cook beef in a large skillet over medium-high heat until no longer pink, then stir in taco seasoning mix and 2 tablespoons water. Cook until thickened.
                        When potatoes are brown and crispy, remove and top with beef, cheese sauce and additional toppings of your choice.
                        Get Cooking: http://www.cooking.com/recipes-and-more/recipes/loaded-nacho-totchos-recipe#ixzz3Zf7isRCu",
                    Ingredients = new List<Ingredient>() 
                    {
                        new Ingredient("One 20- to 30-ounce bag frozen Tater Tots"),
                        new Ingredient("1/2 pound ground beef"),
                    }
                }
            );

            context.SaveChanges();
        }
    }
}
