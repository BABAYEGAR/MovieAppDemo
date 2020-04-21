using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieDemo.Models.DatabaseConnection;
using MovieDemo.Models.Encryption;
using MovieDemo.Models.Entities;
using MovieDemo.Models.Enum;
using Newtonsoft.Json;

namespace MovieDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly MovieDemoDataContext _databaseConnection;
        public AccountController(MovieDemoDataContext databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        /// <summary>
        /// Register get method
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            var appUser = new AppUser();
            return View(appUser);
        }
        /// <summary>
        /// Register post method
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AppUser appUser)
        {
            try
            {
                //check if user exist
                var userExist = _databaseConnection.AppUsers.Where(n => n.Username == appUser.Username).ToList();
                if (userExist.Count <= 0)
                {
                    //hash password with bcrypt
                    var hashPassword = new Hashing().HashPassword(appUser.ConfirmPassword);
                    appUser.Password = hashPassword;
                    appUser.ConfirmPassword = appUser.Password;
                    appUser.CreatedBy = null;
                    appUser.LastModifiedBy = null;
                    appUser.DateLastModified = DateTime.Now;
                    appUser.DateCreated = DateTime.Now;

                    //save transaction
                    _databaseConnection.AppUsers.Add(appUser);
                    _databaseConnection.SaveChanges();

                    TempData["display"] = "Your account has been successfully created!";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Login");
                }

                TempData["display"] = "The username already exist, Try again";
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(appUser);
            }
            catch (Exception e)
            {
                TempData["display"] = e.Message;
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(appUser);
            }
 
        }
        /// <summary>
        /// login get method
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ActionResult Login(string key)
        {
            //logout instruction to clear session and reset database
            if (key == "logout")
            {
                HttpContext.Session.Clear();
                _databaseConnection.Dispose();
            }
            return View();
        }
        /// <summary>
        /// login post method
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AppUser appUser)
        {
            //check if user exist
            var userExist = _databaseConnection.AppUsers.SingleOrDefault(n => n.Username == appUser.Username);
            if (userExist != null && userExist.AppUserId > 0)
            {
                //check if password matches
                var passwordCorrect = new Hashing().ValidatePassword(appUser.Password, userExist.ConfirmPassword);
                if (passwordCorrect)
                {
                    //fetch user with all favorite movies
                    var user = _databaseConnection.AppUsers.Include(n=>n.UserFavouriteMovies).SingleOrDefault(n => n.AppUserId == userExist.AppUserId);
                    
                    //save user object and user id in session
                    HttpContext.Session.SetString("MovieDemoLoggedInUser", JsonConvert.SerializeObject(user));
                    HttpContext.Session.SetString("MovieDemoLoggedInUserId", userExist.AppUserId.ToString());

                    TempData["display"] = "You have successfully signed in.";
                    TempData["notificationtype"] = NotificationType.Success.ToString();
                    return RedirectToAction("Index","Home");
                }

                TempData["display"] = "Incorrect credentials, Try again!";
                TempData["notificationtype"] = NotificationType.Error.ToString();
                return View(appUser);
            }
            TempData["display"] = "Incorrect credentials, Try again!";
            TempData["notificationtype"] = NotificationType.Error.ToString();
            return View(appUser);
        }
        public ActionResult LogOut()
        {
            //logout instruction to clear session and reset database
            return RedirectToAction("Login","Account", new{key = "logout"});
        }
    }
}