using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace MovieDemo.Models.Encryption
{
    public class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetString("MovieDemoLoggedInUser") == null &&
                filterContext.HttpContext.Session.GetString("MovieDemoLoggedInUserId") == null)
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            {"controller", "Account"},
                            {"action", "Login"},
                            {"key", "logout"}
                        });
            }


            base.OnActionExecuting(filterContext);
        }
    }
}