using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieDemo.Models.DatabaseConnection;
using MovieDemo.Models.Services;
using ReflectionIT.Mvc.Paging;

namespace MovieDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly MovieDemoDataContext _databaseConnection;
        public HomeController(MovieDemoDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int page = 1)
        {
            //fetch all movies from json object
            var movies = new APIFactory().GetAllMovies(new AppKey().FetchAllMoviesUrl).Result.ToList();

            //paginate movies
            var model =  PagingList.Create(movies, 10, page);
            return View(model);
        }
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewMovie(long movieId)
        {
            //fetch al movies and query single movie by ID
            var movie = new APIFactory().GetAllMovies(new AppKey().FetchAllMoviesUrl).Result.ToList()
                .SingleOrDefault(n=>n.id == movieId);
            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// Display error page for error 400*,500*
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
