﻿using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class EnergyService : IEnergyService
{
	private readonly IEnergyRepository _energyRepository;
	private readonly IUserRepository _userRepository;

	public EnergyService(IEnergyRepository energyRepository, IUserRepository userRepository)
	{
		_energyRepository = energyRepository;
		_userRepository = userRepository;
	}

	public async Task<IResponce<IList<EnergyViewModel>>> GetAll()
	{
		var responce = new Responce<IList<EnergyViewModel>> { Data = new List<EnergyViewModel>() };
		try
		{
			var energies = _energyRepository.GetAll().Result;

			if (energies.Count() == 0)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			foreach (var energy in energies)
				responce.Data.Add(new EnergyViewModel(energy)
				{
					User = new UserViewModel(energy.User)
				});

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IList<EnergyViewModel>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(EnergyViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var energy = new Energy()
			{
				NormativValue = viewModel.NormativValue,
				DayValue = viewModel.DayValue,
				NightValue = viewModel.NightValue,
				UserId = viewModel.UserId
			};

			responce.Data = await _energyRepository.Create(energy);
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

	public async Task<IResponce<EnergyViewModel>> Edit(EnergyViewModel viewModel)
	{
		var responce = new Responce<EnergyViewModel>();
		try
		{
			var energy = await _energyRepository.Get(viewModel.Id);

			if (energy == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			energy.NightValue = viewModel.NormativValue;
			energy.DayValue = viewModel.DayValue;
			energy.NightValue = viewModel.NightValue;

			await _energyRepository.Update(energy);
			responce.Data = viewModel;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<EnergyViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<EnergyViewModel>> GetById(Guid id)
	{
		var responce = new Responce<EnergyViewModel>();
		try
		{
			var energy = await _energyRepository.Get(id);

			if (energy == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			responce.Data = new EnergyViewModel(energy)
			{
				User = new UserViewModel(energy.User)
			};

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<EnergyViewModel>
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
			var energy = await _energyRepository.Get(id);
			if (energy == null)
			{
				responce.Description = $"Показание с ID {id} не найдено";
				return responce;
			}

			responce.Data = await _energyRepository.DeleteAsync(energy);
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
}