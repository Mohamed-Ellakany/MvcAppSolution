using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcAppDAL.Models;
using MvcAppPL.Helpers;
using MvcAppPL.ViewModels;
using System.Threading.Tasks;

namespace MvcAppPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager ,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }



        #region Register


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new AppUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    Fname = model.Fname,
                    Lname = model.Lname,

                };
                var res = await _userManager.CreateAsync(User, model.Password);

                if (res.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in res.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(model);

        }


        #endregion


        #region Login

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if(User is not null)
                {
                 var res =  await _userManager.CheckPasswordAsync(User, model.Password);

                    if(res)
                    {
                        //login

                      var result=  await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if(result.Succeeded)
                        {
                            return RedirectToAction("Index","Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Password Is Not Correct");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "email is not exist");
                }




            }
            return View(model);
        }
        #endregion


        #region Sign Out

       public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


        #endregion


        #region Forget Password
        
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var User =await _userManager.FindByEmailAsync(model.Email);

                if (User is not null)
                {

                    var token =await _userManager.GeneratePasswordResetTokenAsync(User);

                    var Link = Url.Action("ResetPassword" , "Account" , new {email = User.Email , Token =token },Request.Scheme);

                    // send email
                    var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = model.Email,
                        Body = Link
                    };

                    EmailSend.SendEamil(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not Exist");
                }


            }
            return View("ForgetPassword",model);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion
       


        public IActionResult ResetPassword(string Email , string Token)
        {
            TempData["email"]= Email;
            TempData["token"]= Token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model )
        {
           if(ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;

                var User =await _userManager.FindByEmailAsync(email);
               var Result=  await _userManager.ResetPasswordAsync(User, token,model.NewPassword);
                if(Result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach (var error in Result.Errors) {
                    ModelState.AddModelError (string.Empty, error.Description);
                    }
                }

            }
           return View(model);
        }


    }
}
//P@ssw0rd