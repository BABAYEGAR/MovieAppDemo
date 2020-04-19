using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieDemo.Models.Entities;

namespace MovieDemo.Models.DatabaseConnection
{
    public class MovieDemoDataContext : DbContext
    {
        //database connection context constructor
        public MovieDemoDataContext(DbContextOptions<MovieDemoDataContext> options) : base(options)
        {

        }

        //database db sets/tables
        public  virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<UserFavouriteMovie> UserFavouriteMovies { get; set; }
    }
}
