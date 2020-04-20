using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDemo.Models.DatabaseConnection;
using MovieDemo.Models.Encryption;
using MovieDemo.Models.Entities;
using MovieDemo.Models.Enum;
using MovieDemo.Models.Services;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;

namespace MovieDemo.Controllers
{
    public class UserFavouriteMovieController : Controller
    {
        private readonly MovieDemoDataContext _databaseConnection;
        public UserFavouriteMovieController(MovieDemoDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        /// <summary>
        /// Get all user favorite movies
        /// </summary>
        /// <returns></returns>
        [SessionExpireFilter]
        public IActionResult Index(int page = 1)
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("MovieDemoLoggedInUserId"));
            var userFavMovies = _databaseConnection.UserFavouriteMovies.Where(n => n.AppUserId == userId);
            var movies = new APIFactory().GetAllMovies(new AppKey().FetchAllMoviesUrl).Result.ToList();
            var userMovies = (from a in movies
                join b in userFavMovies on a.id equals b.MovieId
                where b.AppUserId == userId
                select a);
                    
            var model = PagingList.Create(userMovies, 10, page);
            return View(model);
        }
        /// <summary>
        /// This method adds a movies to a user favorite list
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public IActionResult AddMovieToFavoriteList(long movieId)
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("MovieDemoLoggedInUserId"));
            var favoriteMovieExist =
                _databaseConnection.UserFavouriteMovies.Where(n =>
                    n.MovieId == movieId && n.AppUserId == userId).ToList();
            if (favoriteMovieExist.Count <= 0)
            {
                //populate object with values
                var favoriteMovie = new UserFavouriteMovie
                {
                    CreatedBy = userId,
                    LastModifiedBy = userId,
                    DateCreated = DateTime.Now,
                    DateLastModified = DateTime.Now,
                    MovieId = movieId,
                    AppUserId = userId
                };

                //save transaction
                _databaseConnection.UserFavouriteMovies.Add(favoriteMovie);
                _databaseConnection.SaveChanges();

                //fetch user with all favorite movies
                var user = _databaseConnection.AppUsers.Include(n => n.UserFavouriteMovies).SingleOrDefault(n => n.AppUserId == userId);

                //save user object and user id in session
                HttpContext.Session.SetString("MovieDemoLoggedInUser", JsonConvert.SerializeObject(user));

                TempData["display"] = "You have successfully added the movie to your favorite list!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }

            TempData["display"] = "Invalid request!";
            TempData["notificationtype"] = NotificationType.Error.ToString();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// This method removes a movies to a user favorite list
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [SessionExpireFilter]
        public IActionResult RemoveMovieToFavoriteList(long movieId)
        {
            var userId = Convert.ToInt64(HttpContext.Session.GetString("MovieDemoLoggedInUserId"));

            var favoriteMovie =
                _databaseConnection.UserFavouriteMovies.SingleOrDefault(n =>
                    n.MovieId == movieId && n.AppUserId == userId);
            if (favoriteMovie != null && favoriteMovie.UserFavouriteMovieId > 0)
            {
                //save transaction
                _databaseConnection.UserFavouriteMovies.Remove(favoriteMovie);
                _databaseConnection.SaveChanges();


                //fetch user with all favorite movies
                var user = _databaseConnection.AppUsers.Include(n => n.UserFavouriteMovies).SingleOrDefault(n => n.AppUserId == userId);

                //save user object and user id in session
                HttpContext.Session.SetString("MovieDemoLoggedInUser", JsonConvert.SerializeObject(user));

                TempData["display"] = "You have successfully removed the movie from your favorite list!";
                TempData["notificationtype"] = NotificationType.Success.ToString();
                return RedirectToAction("Index");
            }

            TempData["display"] = "Invalid request!";
            TempData["notificationtype"] = NotificationType.Error.ToString();
            return RedirectToAction("Index");
        }
    }
}