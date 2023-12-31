﻿using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class HVSService : IHVSService
{
	private readonly IHVSRepository _hvsRepository;
	private readonly double _normativ = 4.85;
	private readonly double _tarif = 35.78;
	private readonly IUserRepository _userRepository;

	public HVSService(IHVSRepository hvsRepository, IUserRepository userRepository)
	{
		_hvsRepository = hvsRepository;
		_userRepository = userRepository;
	}

	public async Task<IResponce<IList<HVSViewModel>>> GetAll()
	{
		var responce = new Responce<IList<HVSViewModel>> { Data = new List<HVSViewModel>() };
		try
		{
			var hvses = _hvsRepository.GetAll().Result;

			if (hvses.Count() == 0)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			foreach (var hvs in hvses)
				responce.Data.Add(new HVSViewModel(hvs)
				{
					User = new UserViewModel(hvs.User)
				});

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IList<HVSViewModel>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(HVSViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var hvs = new HVS
			{
				CurrentValue = viewModel.CurrentValue,
				UserId = viewModel.UserId
			};

			var user = await _userRepository.GetById(viewModel.UserId);
			responce.Data = await _hvsRepository.Create(await GetStatisticResult(hvs, user));

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<bool>
			{
				Description = $"[Create] : {e.Message}",
				Data = false
			};
		}
	}

	public async Task<IResponce<HVSViewModel>> Edit(HVSViewModel viewModel)
	{
		var responce = new Responce<HVSViewModel>();
		try
		{
			var hvs = await _hvsRepository.Get(viewModel.Id);

			if (hvs == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			hvs.CurrentValue = viewModel.CurrentValue;

			await _hvsRepository.Update(hvs);
			responce.Data = viewModel;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<HVSViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<HVSViewModel>> GetById(Guid id)
	{
		var responce = new Responce<HVSViewModel>();
		try
		{
			var hvs = await _hvsRepository.Get(id);

			if (hvs == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			responce.Data = new HVSViewModel(hvs)
			{
				User = new UserViewModel(hvs.User)
			};

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<HVSViewModel>
			{
				Description = $"[GetById] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Delete(Guid id)
	{
		var responce = new Responce<bool>();
		try
		{
			var hvs = await _hvsRepository.Get(id);
			if (hvs == null)
			{
				responce.Description = $"Показание с ID {id} не найдено";
				return responce;
			}

			responce.Data = await _hvsRepository.DeleteAsync(hvs);
			return responce;
		}
		catch (Exception e)
		{
			return new Responce<bool>
			{
				Description = $"[Delete] : {e.Message}",
				Data = false
			};
		}
	}

	private async Task<HVS> GetStatisticResult(HVS currentHVS, User user)
	{
		var lastHVS = user.HVSStatistic.OrderBy(x => x.CreatedDate).LastOrDefault();

		if (lastHVS == null)
			return currentHVS;

		if (user.HasHvsMeter)
			lastHVS.TotalResult += currentHVS.CurrentValue - lastHVS.CurrentValue > 0
				? (currentHVS.CurrentValue - lastHVS.CurrentValue) * _tarif
				: 0;
		else
			lastHVS.TotalResult += user.PeopleCount * _normativ * _tarif;

		await _hvsRepository.Update(lastHVS);

		return currentHVS;
	}
}