using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CMS.Models.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoginController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            Response.Cookies.Delete("user_Lognstatus");

            DleteCookie("user_Lognstatus", "");
            DleteCookie("CMS", "");
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("user_Lognstatus");
            HttpContext.Session.Remove("CMS");
            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Index(LoginView model)
        //{
        //	TempData["ErrorMessage"] = string.Empty;
        //	if (ModelState.IsValid)
        //	{
        //		//var url = "/cms3/ui/en";
        //		var url = "category.html";
        //		validate v1 = new validate();
        //		//ClaimsIdentity claimsIdentity;			
        //		var Iresult = v1.ValidateCredentials(model.UserName, model.Password);
        //		if (Iresult.claimsid == null)
        //		{
        //			TempData["ErrorMessage"] = Iresult.message;
        //			return View("Index");
        //		}
        //		try
        //		{

        //			await HttpContext.SignOutAsync(
        //					CookieAuthenticationDefaults.AuthenticationScheme);

        //			await HttpContext.SignInAsync(
        //							CookieAuthenticationDefaults.AuthenticationScheme,
        //							new ClaimsPrincipal(Iresult.claimsid),
        //							new AuthenticationProperties
        //							{
        //								IsPersistent = true,
        //								ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
        //							});

        //			if (_httpContextAccessor.HttpContext.Request.Cookies["user_Lognstatus"] == null || _httpContextAccessor.HttpContext.Request.Cookies["user_Lognstatus"] == "Logout")
        //				Set("user_Lognstatus", "Login", 60);

        //			return this.Redirect(url);
        //		}
        //		catch (Exception ex)
        //		{
        //			string err = ex.Message;
        //			TempData["ErrorMessage"] = "An error occurred. Please contact Administrator :Error" + ex.Message;
        //			return View("Index");
        //		}
        //	}
        //	//validate.testdir();
        //	return View("Index");
        //}


        [System.Web.Mvc.ValidateInput(true)]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginView model)
        {
            TempData["ErrorMessage"] = string.Empty;
            if (ModelState.IsValid)
            {
                //var url = "/cms3/ui/en";
                //var url = "/cms3/category.html";
                //	validate v1 = new validate();
                validate ldabV1 = new validate();
                //ClaimsIdentity claimsIdentity;			
                var Iresult = await ldabV1.ValidateUserUsinLdapApi(model.UserName, model.Password);
                if (Iresult.claimsid == null)
                {
                    TempData["ErrorMessage"] = Iresult.message;
                    TempData["ErrorMessage"] = "Please Enter A Valid User Name and Password!";
                    return View("Index");
                    ///return View("Index");
                }
                try
                {


                    Response.Cookies.Delete("user_Lognstatus");

                    DleteCookie("user_Lognstatus", "");
                    DleteCookie("CMS", "");
                    HttpContext.Session.Clear();
                    HttpContext.Session.Remove("user_Lognstatus");
                    HttpContext.Session.Remove("CMS");
                    await HttpContext.SignOutAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                                    CookieAuthenticationDefaults.AuthenticationScheme,
                                    new ClaimsPrincipal(Iresult.claimsid)
                                    , new AuthenticationProperties
                                    {
                                        AllowRefresh = false,
                                        ExpiresUtc = DateTime.UtcNow.AddMinutes(15)
                                        //IsPersistent = false
                                    });

                    if (_httpContextAccessor.HttpContext.Request.Cookies["user_Lognstatus"] == null || _httpContextAccessor.HttpContext.Request.Cookies["user_Lognstatus"] == "Logout")
                        Set("user_Lognstatus", Guid.NewGuid().ToString(), 15);

                    //return RedirectToAction("index", "ui",new { language = "ar" });///this.Redirect(url);
                    return Redirect("~/cms3/");
                }
                catch (Exception ex)
                {
                    string err = ex.Message;
                    TempData["ErrorMessage"] = "An error occurred. Please contact Administrator :Error" + ex.Message;
                    return View("Index");
                }
            }
            //validate.testdir();
            return View("Index");
        }


        public void Set(string key, string value, double expireTime = 15)
        {
            CookieOptions option = new CookieOptions();
            //if (expireTime.HasValue)
            option.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(expireTime)); //DateTime.Now.AddMinutes(10);
            option.IsEssential = true;
            //else
            //	option.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(expireTime)); //DateTime.Now.AddMinutes(60);
            Response.Cookies.Append(key, value, option);
        }
        public IActionResult AccessDenied()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public bool IsUserAuthenticated()
        {


            bool checkUser = AppHttpContext.Current.User.Identity.IsAuthenticated;
            return checkUser;
            //return Json(new { Authenticated = checkUser });




        }
        public void DleteCookie(string key, string value)
        {
            CookieOptions option = new CookieOptions();
            //if (expireTime.HasValue)
            option.Expires = new DateTimeOffset(DateTime.Now.AddDays(-1)); //DateTime.Now.AddMinutes(10);
           // option.IsEssential = true;
            //else
            //	option.Expires = new DateTimeOffset(DateTime.Now.AddMinutes(expireTime)); //DateTime.Now.AddMinutes(60);
            Response.Cookies.Append(key, value, option);
        }
    }
}