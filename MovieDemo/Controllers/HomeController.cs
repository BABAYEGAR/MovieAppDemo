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
            var movies = new APIFactory().GetAllMovies(new AppKey().FetchAllMoviesUrl).Result.ToList();
            var model =  PagingList.Create(movies, 10, page);
            return View(model);
        }
        /// <summary>
        /// Home page
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewMovie(long movieId)
        {
            var movie = new APIFactory().GetAllMovies(new AppKey().FetchAllMoviesUrl).Result.ToList()
                .SingleOrDefault(n=>n.id == movieId);
            return View(movie);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
