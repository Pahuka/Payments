using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsWebApp.Controllers;

public class StatisticController : Controller
{
	private readonly IEnergyService _energyService;
	private readonly IGVSService _gvsService;
	private readonly IHVSService _hvsService;
	private readonly IUserService _userService;

	public StatisticController(IUserService userService, IEnergyService energyService, IHVSService hvsService,
		IGVSService gvsService)
	{
		_userService = userService;
		_energyService = energyService;
		_hvsService = hvsService;
		_gvsService = gvsService;
	}

	[HttpGet]
	public IActionResult CreateEnergyStatistic(Guid userId)
	{
		return View(new EnergyViewModel() { UserId = userId });
	}

	[HttpPost]
	public async Task<IActionResult> CreateEnergyStatistic(EnergyViewModel energyViewModel)
	{
		var responce = await _energyService.Create(energyViewModel);
		if (responce.Data)
			return RedirectToAction("GetAllEnergyStatistic");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> GetAllEnergyStatistic()
	{
		var responce = await _energyService.GetAll();
		if (responce.Data != null)
			return View(responce.Data.ToList());


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> GetUser(string login)
	{
		var responce = await _userService.GetByLogin(login);
		if (responce.Data != null)
			return View(responce.Data);
		return RedirectToAction("Error");
	}
}