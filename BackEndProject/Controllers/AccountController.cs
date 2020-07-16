using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.Helpers;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(RegisterVM signup)
        {
            if (!ModelState.IsValid) return View(signup);
            AppUser user = new AppUser
            {
                UserName = signup.Username,
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, signup.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(signup);
            }
            await _userManager.AddToRoleAsync(user, Helper.Roles.Member.ToString());
            if (signup.IsChecked == true)
            {
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);
            AppUser logUser = await _userManager.FindByNameAsync(login.Username);
            if (logUser == null)
            {
                ModelState.AddModelError("", "Username or password is not valid");
                return View(login);
            }
            if (logUser.IsDeleted == true)
            {
                ModelState.AddModelError("", "This account has been blocked");
                return View();
            }
            if (login.IsChecked == true){
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(logUser, login.Password, true, true);
                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError("", "Username or password is not valid");
                    return View(login);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(logUser, login.Password, false, true);
                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError("", "Username or password is not valid");
                    return View(login);
                }
                return RedirectToAction("Index", "Home");
            }
            
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task CreateRole()
        {
            if(!await _roleManager.RoleExistsAsync(Helper.Roles.Admin.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Roles.Admin.ToString()));
            }
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.Member.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Roles.Member.ToString()));
            }
            if (!await _roleManager.RoleExistsAsync(Helper.Roles.CourseOwner.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Roles.CourseOwner.ToString()));
            }
        }
    }
}