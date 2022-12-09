using Microsoft.AspNetCore.Mvc;
using OMTBal.IServices;

namespace OnlineMockTest.Controllers
{
    public class SecureController : BaseController
    {
        public SecureController(IUserService userService) : base(userService)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
