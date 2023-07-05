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

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> GetCurrentUserStatistic()
	{
		var responce = await _userService.GetByLogin(HttpContext.User.Identity.Name);
		if (responce.Data != null)
			return View(responce.Data);
		return RedirectToAction("Error");
	}

	#region Горячая вода

	[HttpGet]
	public async Task<IActionResult> CreateGVSStatistic(Guid userId)
	{
		var user = await _userService.GetById(userId);
		return View(new GVSViewModel { User = user.Data, UserId = userId });
	}

	[HttpPost]
	public async Task<IActionResult> CreateGVSStatistic(GVSViewModel gvsViewModel)
	{
		var responce = await _gvsService.Create(gvsViewModel);
		if (responce.Data)
			return RedirectToAction("GetAllHVSStatistic");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> GetAllGVSStatistic()
	{
		var responce = await _gvsService.GetAll();
		if (responce.Data != null)
			return RedirectToAction("GetCurrentUserStatistic");


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	#endregion

	#region Холодная вода

	[HttpGet]
	public async Task<IActionResult> CreateHVSStatistic(Guid userId)
	{
		var user = await _userService.GetById(userId);
		return View(new HVSViewModel { User = user.Data, UserId = userId });
	}

	[HttpPost]
	public async Task<IActionResult> CreateHVSStatistic(HVSViewModel hvsViewModel)
	{
		var responce = await _hvsService.Create(hvsViewModel);
		if (responce.Data)
			return RedirectToAction("GetAllHVSStatistic");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> GetAllHVSStatistic()
	{
		var responce = await _hvsService.GetAll();
		if (responce.Data != null)
			return RedirectToAction("GetCurrentUserStatistic");


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	#endregion

	#region Электроэнергия

	[HttpGet]
	public async Task<IActionResult> CreateEnergyStatistic(Guid userId)
	{
		var user = await _userService.GetById(userId);
		return View(new EnergyViewModel { User = user.Data, UserId = userId });
	}

	[HttpPost]
	public async Task<IActionResult> CreateEnergyStatistic(EnergyViewModel energyViewModel)
	{
		var responce = await _energyService.Create(energyViewModel);
		if (responce.Data)
			return RedirectToAction("GetCurrentUserStatistic");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	[HttpGet]
	public async Task<IActionResult> GetAllEnergyStatistic()
	{
		var responce = await _energyService.GetAll();
		if (responce.Data != null)
			return RedirectToAction("GetCurrentUserStatistic");


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	#endregion
}