using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndProject.DAL;
using BackEndProject.Models;
using BackEndProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEndProject.Areas.AdminEduHome.Controllers
{
	[Area("AdminEduHome")]
	public class AccountController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UserList()
		{
			List<AppUser> allUsers = _userManager.Users.ToList();
			List<UserVM> usersVM = new List<UserVM>();
			foreach (AppUser user in allUsers)
			{
				if (user.IsDeleted == false)
				{
					UserVM newUser = new UserVM
					{
						Id = user.Id,
						Username = user.UserName,
						Role = (await _userManager.GetRolesAsync(user))[0]
					};
					usersVM.Add(newUser);
				}
			}
			return View(usersVM);
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(string username)
		{
			if (username == null) return NotFound();
			AppUser user = await _userManager.FindByNameAsync(username);
			if (user == null) return NotFound();
			if (user.IsDeleted == true) return NotFound();
			UserVM userVM = new UserVM
			{
				Username = user.UserName,
				Role = (await _userManager.GetRolesAsync(user))[0]
			};
			return View(userVM);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Edit")]
		public async Task<IActionResult> EditUser(string username)
		{
			AppUser appUser = await _userManager.FindByNameAsync(username);
			var oldRole = (await _userManager.GetRolesAsync(appUser))[0];
			await _userManager.RemoveFromRoleAsync(appUser, oldRole);
			var role = Request.Form["roles"];
			await _userManager.AddToRoleAsync(appUser, role);
			return RedirectToAction("UserList");
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(string username)
		{
			if (username == null) return NotFound();
			AppUser user = await _userManager.FindByNameAsync(username);
			if (user == null) return NotFound();
			UserVM userVM = new UserVM
			{
				Username = user.UserName,
				Role = (await _userManager.GetRolesAsync(user))[0]
			};
			return View(userVM);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Delete")]
		public async Task<IActionResult> DeleteUser(string username)
		{
			AppUser user = await _userManager.FindByNameAsync(username);
			user.IsDeleted = true;
			await _userManager.UpdateAsync(user);
			return RedirectToAction("UserList");
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> BlockedList()
		{
			List<AppUser> allUsers = _userManager.Users.ToList();
			List<UserVM> usersVM = new List<UserVM>();
			foreach (AppUser user in allUsers)
			{
				if (user.IsDeleted == true)
				{
					UserVM newUser = new UserVM
					{
						Id = user.Id,
						Username = user.UserName,
						Role = (await _userManager.GetRolesAsync(user))[0]
					};
					usersVM.Add(newUser);
				}
			}
			return View(usersVM);
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Unblock(string username)
		{
			if (username == null) return NotFound();
			AppUser user = await _userManager.FindByNameAsync(username);
			if (user == null) return NotFound();
			UserVM userVM = new UserVM
			{
				Username = user.UserName,
				Role = (await _userManager.GetRolesAsync(user))[0]
			};
			return View(userVM);
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ActionName("Unblock")]
		public async Task<IActionResult> UnblockUser(string username)
		{
			AppUser user = await _userManager.FindByNameAsync(username);
			user.IsDeleted = false;
			await _userManager.UpdateAsync(user);
			return RedirectToAction("UserList");
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Register()
		{
			return View();
		}
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterVM register)
		{
			if (!ModelState.IsValid) return View(register);
			AppUser user = new AppUser
			{
				UserName = register.Username
			};
			IdentityResult identityResult = await _userManager.CreateAsync(user, register.Password);
			if (!identityResult.Succeeded)
			{
				foreach (var error in identityResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return View();
			}
			var role = Request.Form["roles"];
			await _userManager.AddToRoleAsync(user, role);
			return RedirectToAction("UserList");
		}
		[Authorize(Policy = "CourseManager")]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home", new { area = "" });
		}
	}
}