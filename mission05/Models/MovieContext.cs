using System;
using Microsoft.EntityFrameworkCore;

namespace mission05.Models
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base (options)
        {
        }

        //Create tables in database
        public DbSet<NewMovie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Seed Category table
            mb.Entity<Category>().HasData
            (
                new Category { CategoryID = 1, CategoryName = "Action/Adventure"},
                new Category { CategoryID = 2, CategoryName = "Comedy" },
                new Category { CategoryID = 3, CategoryName = "Drama" },
                new Category { CategoryID = 4, CategoryName = "Family" },
                new Category { CategoryID = 5, CategoryName = "Drama" },
                new Category { CategoryID = 6, CategoryName = "Horrer/Suspense" },
                new Category { CategoryID = 7, CategoryName = "Miscellaneous" },
                new Category { CategoryID = 8, CategoryName = "Television" },
                new Category { CategoryID = 9, CategoryName = "VHS" }
             );

            //Seed Movie table
            mb.Entity<NewMovie>().HasData
            (
                new NewMovie
                {
                    MovieID = 1,
                    CategoryID = 1,
                    Title = "Batman Begins",
                    Year = 2005,
                    Director = "Christopher Nolan",
                    Rating = "PG-13",
                    Edited = false,
                },

                new NewMovie
                {
                    MovieID = 2,
                    CategoryID = 1,
                    Title = "Dark Knight, The",
                    Year = 2008,
                    Director = "Christopher Nolan",
                    Rating = "PG-13",
                    Edited = false,
                },

                new NewMovie
                {
                    MovieID = 3,
                    CategoryID = 1,
                    Title = "Dark Knight Rises, The",
                    Year = 2012,
                    Director = "Christopher Nolan",
                    Rating = "PG-13",
                    Edited = false,
                }
            );
        }
    }
}
