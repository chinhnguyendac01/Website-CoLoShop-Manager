using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _19T1021302.BusinessLayers;

namespace _19T1021302.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Trang đăng nhập vào hệ thống
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName="",string password="")
        {
            ViewBag.UserName = userName;
            if(string.IsNullOrWhiteSpace(userName) || (string.IsNullOrWhiteSpace(password)))
            {
                ModelState.AddModelError("", "Thông tin không đủ");
                return View();
            }
            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if (userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }
            //Ghi cookie cho phiên đăng nhập
            string cookieString = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieString, false);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(string userName, string oldPassword, string newPassword,string confirmPassword)
        {
            if(string.IsNullOrEmpty(confirmPassword)||string.IsNullOrEmpty(oldPassword)||string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("", "Thông tin không đủ");
                return View();
            }
            if (newPassword != confirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu không trùng nhau");
                return View();
            }
            var result = UserAccountService.ChangePassword(AccountTypes.Employee, userName, oldPassword, newPassword);
            if (result ==false)
            {
                ModelState.AddModelError("", "Không thành công");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}