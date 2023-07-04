using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace PaymentsWebApp.Controllers;

public class StatisticController : Controller
{
	private readonly IStatisticService _statisticService;
	private readonly IUserService _userService;

	public StatisticController(IStatisticService statisticService, IUserService userService)
	{
		_statisticService = statisticService;
		_userService = userService;
	}

	[HttpGet]
	public async Task<IActionResult> CreateStatistic()
	{
		var user = await _userService.GetByLogin("1");
		return View(new StatisticViewModel(){ User = user.Data });
	}

	[HttpPost]
	public async Task<IActionResult> CreateStatistic(StatisticViewModel statisticViewModel)
	{
		var responce = await _statisticService.Create(statisticViewModel);
		if (responce.Data)
			return RedirectToAction("GetAllStatistics");

		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}

	public IActionResult Error()
	{
		ViewBag.Message = TempData["Message"];
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> GetAllStatistics()
	{
		var responce = await _statisticService.GetAll();
		if (responce.Data != null)
			return View(responce.Data.ToList());


		TempData["Message"] = responce.Description;
		return RedirectToAction("Error");
	}
}