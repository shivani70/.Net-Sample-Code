using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OMTBal.IServices;
using OMTDal.Entity;
using OMTModel;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineMockTest.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthenticationService _authenticationService; 
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginService _loginService;
        private const string EmailOtp = "1111";
        public LoginController(ILogger<LoginController> logger, ILoginService loginService,IAuthenticationService authService)
        {
            this._authenticationService = authService;
            this._loginService = loginService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Login()
        {
        
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginOMT loginOMT)
        {
            try
            {
                var loginResponse = this._loginService.Login(loginOMT.Email, loginOMT.Password);
                if (loginResponse.isLogin)
                {
                    User userdetail = loginResponse.userdetail;
                    TempData["message"] = " " + loginOMT.Email;
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("UserId", userdetail.Id.ToString()));
                    claims.Add(new Claim("Email", userdetail.Email.ToString()));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    AuthenticationProperties authProp = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                        IsPersistent = true,
                        AllowRefresh = true
                    };
                    HttpContext.User.AddIdentity(claimsIdentity);
                    await _authenticationService.SignInAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProp);
                    await _authenticationService.AuthenticateAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
                    return RedirectToAction("Index", "Secure");
                }
                else
                {
                    TempData["error"] = "Incorrect Id and password";
                }
            }
            catch(Exception ex)
            {

            }
        
            return View(loginOMT);
        }
        
        [HttpGet]
        public IActionResult ConfirmEmail()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(confirmEmailOMT emailOMT)
        {
            var loginResponse = this._loginService.Email(emailOMT.Email);
            if (loginResponse.isEmail)
            {
                TempData["message"] = loginResponse.message + " " + emailOMT.Email;
                TempData["Email"] = emailOMT.Email;
                return RedirectToAction("OtpConfirm");
            }
            else
            {
                TempData["error"] = loginResponse.message;
                
            }

            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword()
        {
            return RedirectToAction("ConfirmEmail");
        }
        [HttpGet]
        public IActionResult OtpConfirm()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult OtpConfirm(OTPConfirmOMT model)
        {
            if( model.OTP == EmailOtp)
            {
                TempData["Email"]=model.Email;
                return RedirectToAction("ResetPassword");
            }
            else
            {
                TempData["error"] = "OTP is Wrong";
            }
            
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordOMT Model)
        {
           
            if (Model.Password==Model.ConfirmPassword)
            {
                var loginResponse = this._loginService.UpdatePassword(Model.Email, Model.Password);
                return RedirectToAction("Login");
            }
            else
            {
                TempData["error"] = "Confirm Password is wrong";
            }
            return View();
        }
    }
}
