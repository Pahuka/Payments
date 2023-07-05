using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsWeb.Controllers;

public class UserController : Controller
{
	private readonly IUserService _userService;

	public UserController(IUserService userService)
	{
		_userService = userService;
	}

	[Authorize(Roles = "Administrator")]
	[HttpGet]
	public IActionResult CreateUser()
	{
		return View(new UserViewModel());
	}

	[Authorize(Roles = "Administrator")]
	[HttpPost]
	public async Task<IActionResult> CreateUser(UserViewModel userViewModel)
	{
		var responce = await _userService.Create(userViewModel);
		if (responce.Data)
			return RedirectToAction("GetAllUsers");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}

	[Authorize(Roles = "Administrator")]
	[HttpGet]
	public async Task<IActionResult> GetAllUsers()
	{
		var responce = await _userService.GetAll();
		if (responce.Data != null)
			return View(responce.Data.ToList());


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}
}