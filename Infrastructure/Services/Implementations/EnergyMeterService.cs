using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class EnergyMeterService : IEnergyMeterService
{
	private readonly IEnergyMeterRepository _energyMeterRepository;
	private readonly IStatisticRepository _statisticRepository;

	public EnergyMeterService(IEnergyMeterRepository energyMeterRepository, IStatisticRepository statisticRepository)
	{
		_energyMeterRepository = energyMeterRepository;
		_statisticRepository = statisticRepository;
	}

	public async Task<IResponce<IList<EnergyMeterViewModel>>> GetAll()
	{
		var responce = new Responce<IList<EnergyMeterViewModel>> { Data = new List<EnergyMeterViewModel>() };
		try
		{
			var energyMeters = _energyMeterRepository.GetAll().Result;

			if (energyMeters.Count() == 0)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			foreach (var energyMeter in energyMeters)
				responce.Data.Add(new EnergyMeterViewModel(energyMeter)
				{
					Statistic = new StatisticViewModel(energyMeter?.Statistic)
				});

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IList<EnergyMeterViewModel>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(EnergyMeterViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var energyMeter = new EnergyMeter()
			{
				CurrentValueDay = viewModel.CurrentValueDay,
				CurrentValueNight = viewModel.CurrentValueNight,
				Statistic = await _statisticRepository.Get(viewModel.Statistic.Id)
			};

			responce.Data = await _energyMeterRepository.Create(energyMeter);

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

	public async Task<IResponce<EnergyMeterViewModel>> Edit(EnergyMeterViewModel viewModel)
	{
		var responce = new Responce<EnergyMeterViewModel>();
		try
		{
			var energyMeter = await _energyMeterRepository.Get(viewModel.Id);

			if (energyMeter == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			energyMeter.CurrentValueDay = viewModel.CurrentValueDay;
			energyMeter.CurrentValueNight = viewModel.CurrentValueNight;
			energyMeter.Statistic = await _statisticRepository.Get(viewModel.Statistic.Id);

			await _energyMeterRepository.Update(energyMeter);
			responce.Data = viewModel;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<EnergyMeterViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<EnergyMeterViewModel>> GetById(Guid id)
	{
		var responce = new Responce<EnergyMeterViewModel>();
		try
		{
			var energyMeter = await _energyMeterRepository.Get(id);

			if (energyMeter == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			responce.Data = new EnergyMeterViewModel(energyMeter)
			{
				Statistic = new StatisticViewModel(energyMeter?.Statistic)
			};

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<EnergyMeterViewModel>
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
			var energyMeter = await _energyMeterRepository.Get(id);
			if (energyMeter == null)
			{
				responce.Description = $"Показание с ID {id} не найдено";
				return responce;
			}

			responce.Data = await _energyMeterRepository.DeleteAsync(energyMeter);
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