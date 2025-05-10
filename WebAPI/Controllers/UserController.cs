using AspNetCoreIdentityApp.Web.Extensions;
using AutoMapper;
using Dto.ApplicationUsers;
using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Abstracts;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userService.GetCurrentUserAsync().Result.Data;
            if (currentUser == null)
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
            var userViewModel = new ApplicationUser
            {
                Email = currentUser!.Email,
                UserName = currentUser!.UserName,
                PhoneNumber = currentUser!.PhoneNumber,
            };
            return View(userViewModel);
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserDto request, string? returnUrl)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (!ModelState.IsValid)
            {
                return View();
            }

            returnUrl ??= Url.Action("Index", "Home");

            var result = await _userService.LoginAsync(request);
            if (await result.Success)
            {
                return Redirect(returnUrl!);
            }

            if ( result.Message != null )
            {
                if (await result.Message == "LockedOut")
                {
                    ModelState.AddModelError(string.Empty, "User is locked out.");
                    return View();
                }
                
            }
            ModelState.AddModelErrorList(new List<string>() { $"Email veya şifre yanlış. Kalan giriş hakkı: {3 - await _userManager.GetAccessFailedCountAsync(hasUser!)}" });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userService.RegisterAsync(request);

            if (await result.Success)
            {
                TempData["SuccessMessage"] = "Üyelik kaydınız başarıyla gerçekleşmiştir!";
                return RedirectToAction(nameof(UserController.SignUp));
            }
            ModelState.AddModelErrorList(await result.Messages);
            return View();
        }


        public async Task<IActionResult> SignOut(string email)
        {
            await _userService.LogOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto request)
        {
            var result = await _userService.ChangePasswordAsync(request);
            if (await result.Success)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";
                return RedirectToAction(nameof(UserController.ChangePassword));
            }
            ModelState.AddModelErrorList(await result.Messages);

            return View();
        }

        public async Task<IActionResult> EditUser()
        {
            ViewBag.genderList = new SelectList(Enum.GetNames(typeof(Gender)));

            var currentUser = await _userService.GetCurrentUserAsync().Result.Data;

            var userEditViewModel = new EditUserDto()
            {
                UserName = currentUser!.UserName!,
                Email = currentUser.Email!,
                PhoneNumber = currentUser.PhoneNumber!,
                BirthDate = currentUser!.BirthDate,
                Gender = currentUser.Gender
            };

            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userService.EditUserInformationAsync(request);
            if (await result.Success)
            {
                TempData["SuccessMessage"] = "Kullanıcı bilgileriniz başarıyla güncellenmiştir.";
                return RedirectToAction(nameof(UserController.EditUser));
            }
            ModelState.AddModelErrorList(await result.Messages);
            return View();
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(UserForgotPasswordDto request)
        {
            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Bu email adresine sahip kullanıcı bulunamamıştır.");
                return View();
            }
            var result = await _userService.SendPasswordResetLinkAsync(request);
            if (await result.Success)
            {
                TempData["SuccessMessage"] = "Şifre sıfırlama linki email adresinize gönderilmiştir.";
                return RedirectToAction(nameof(UserController.ForgotPassword));
            }
            ModelState.AddModelErrorList(await result.Messages);
            return View();

        }

        public IActionResult ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(UserResetPasswordDto request)
        {
            var userId = Guid.Parse(TempData["userId"].ToString());
            var token = TempData["token"].ToString();


            var result = await _userService.ResetUserPasswordAsync(request, userId, token);
            

            if (await result.Success)
            {
                TempData["SuccessMessage"] = "Şifreniz başarıyla değiştirilmiştir.";
            }
            else
            {
                ModelState.AddModelErrorList(await result.Messages);
            }

            return View();
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            string message = string.Empty;

            message = "Bu sayfaya erişiminiz yoktur! Bir yanlışlık olduğunu düşünüyorsanız destek ekibimizle iletişime geçin.";
            ViewBag.message = message;

            return View();
        }
    }
}
