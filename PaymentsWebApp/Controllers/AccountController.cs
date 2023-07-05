using System.Security.Claims;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PaymentsWebApp.Controllers;

public class AccountController : Controller
{
	private readonly IAccountService _accountService;

	public AccountController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Register(UserViewModel model)
	{
		if (ModelState["FirstName"].ValidationState == ModelValidationState.Valid &&
		    ModelState["LastName"].ValidationState == ModelValidationState.Valid &&
		    ModelState["Login"].ValidationState == ModelValidationState.Valid &&
		    ModelState["Password"].ValidationState == ModelValidationState.Valid &&
		    ModelState["PeopleCount"].ValidationState == ModelValidationState.Valid)
		{
			var responce = await _accountService.Register(model);

			if (responce.Data == null)
			{
				TempData["Message"] = responce.Description;
				return RedirectToAction("Error");
			}

			if (responce.Data.IsAuthenticated)
			{
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(responce.Data));

				return RedirectToAction("GetCurrentUserStatistic", "Statistic");
			}
		}

		return View(model);
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Login(UserViewModel model)
	{
		if (ModelState["Login"].ValidationState == ModelValidationState.Valid &&
		    ModelState["Password"].ValidationState == ModelValidationState.Valid)
		{
			var response = await _accountService.Login(model);
			if (response.Data.IsAuthenticated)
			{
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(response.Data));

				return RedirectToAction("GetCurrentUserStatistic", "Statistic");
			}
		}

		return View(model);
	}

	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return RedirectToAction("Index", "Home");
	}

	[HttpPost]
	public async Task<IActionResult> ChangePassword(UserViewModel model)
	{
		if (ModelState.IsValid)
		{
			var response = await _accountService.ChangePassword(model);
			return Json(new { description = response.Description });
		}

		var modelError = ModelState.Values.SelectMany(v => v.Errors);

		return StatusCode(StatusCodes.Status500InternalServerError, new { modelError.FirstOrDefault().ErrorMessage });
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}
}