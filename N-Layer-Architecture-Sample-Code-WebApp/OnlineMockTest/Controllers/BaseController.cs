using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OMTBal.IServices;
using System;
using System.Linq;
using System.Security.Claims;

namespace OnlineMockTest.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService UserService;
        public BaseController(IUserService userService)
        {
            UserService = userService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context); 
            var claimList = context.HttpContext.User.Claims;
            if (claimList != null && claimList.Count() > 0)
            {
                int userId = Convert.ToInt32(claimList.Where(d => d.Type == "UserId").FirstOrDefault().Value);
                ViewBag.User = UserService.GetUserById(userId); 
            }
            else
            {
                RedirectToAction("Login", "Login");
            }
        }
    }
}
