using Company_Tour_Management_System.Models;
using Company_Tour_Management_System.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company_Tour_Management_System.Controllers
{
    
    public class AccountController : Controller
    {
        #region Ctro
        private readonly IMainService obj;
        public AccountController(IMainService obj)
        {
            this.obj = obj;
        }
        #endregion
        #region Methods
        public IActionResult SignUp()
        {
            return View();
        }
      
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await obj.UserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var ap in result.Errors)
                    {
                        ModelState.AddModelError("", ap.Description);
                    }

                    return View(userModel);
                }
                var loginUser = new SignInModel();
                loginUser.Password = userModel.Password;
                loginUser.Email = userModel.Email;
                var result1 = await obj.PasswordSignInAsync(loginUser);
                if (result1.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.Clear();
            }
            return View();
        }
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel userModel,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await obj.PasswordSignInAsync(userModel);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Error");

            }

            return View(userModel);
        }
      
        public IActionResult Logout()
        {
            obj.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}
