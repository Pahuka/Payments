using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class StatisticService : IStatisticService
{
	private readonly IStatisticRepository _statisticRepository;
	private readonly IUserRepository _userRepository;
	private readonly IGVSRepository _gvsRepository;
	private readonly IHVSRepository _hvsRepository;
	private readonly IEnergyRepository _energyRepository;
	private readonly IEnergyMeterRepository _energyMeterRepository;

	public StatisticService(IStatisticRepository statisticRepository, IUserRepository userRepository,
		IGVSRepository gvsRepository, IHVSRepository hvsRepository, IEnergyRepository energyRepository,
		IEnergyMeterRepository energyMeterRepository)
	{
		_statisticRepository = statisticRepository;
		_userRepository = userRepository;
		_gvsRepository = gvsRepository;
		_hvsRepository = hvsRepository;
		_energyRepository = energyRepository;
		_energyMeterRepository = energyMeterRepository;
	}

	public async Task<IResponce<IList<StatisticViewModel>>> GetAll()
	{
		var responce = new Responce<IList<StatisticViewModel>> { Data = new List<StatisticViewModel>() };
		try
		{
			var statistics = _statisticRepository.GetAll().Result;

			if (statistics.Count() == 0)
			{
				responce.Description = "Статистика не найдена";
				return responce;
			}

			foreach (var statistic in statistics)
				responce.Data.Add(new StatisticViewModel
				{
					//Id = statistic.Id,
					//HVS = statistic.HVS != null ? new HVSViewModel(statistic.HVS) {}: null,
					//GVS = statistic.GVS,
					//Energy = statistic.Energy,
					//EnergyMeter = statistic.EnergyMeter,
					//User = statistic.User
				});

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IList<StatisticViewModel>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(StatisticViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var statistic = new Statistic
			{
				User = viewModel.User != null ? await _userRepository.Get(viewModel.User.Login) : null,
				Energy = viewModel.Energy != null ? await _energyRepository.Get(viewModel.Energy.Id) : null,
				HVS = viewModel.HVS != null ? await _hvsRepository.Get(viewModel.HVS.Id) : null,
				GVS = viewModel.GVS != null ? await _gvsRepository.Get(viewModel.GVS.Id) : null,
				EnergyMeter = viewModel.EnergyMeter != null ? await _energyMeterRepository.Get(viewModel.EnergyMeter.Id) : null,
			};

			responce.Data = await _statisticRepository.Create(statistic);

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

	public async Task<IResponce<StatisticViewModel>> Edit(StatisticViewModel viewModel)
	{
		var responce = new Responce<StatisticViewModel>();
		try
		{
			var statistic = await _statisticRepository.Get(viewModel.Id);

			if (statistic == null)
			{
				responce.Description = "Статистика не найдена";
				return responce;
			}

			//statistic.User = viewModel.User;
			//statistic.HVS = viewModel.HVS;
			//statistic.GVS = viewModel.GVS;
			//statistic.Energy = viewModel.Energy;
			//statistic.EnergyMeter = viewModel.EnergyMeter;

			await _statisticRepository.Update(statistic);
			responce.Data = viewModel;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<StatisticViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<StatisticViewModel>> GetById(Guid id)
	{
		var responce = new Responce<StatisticViewModel>();
		try
		{
			var statistic = await _statisticRepository.Get(id);

			if (statistic == null)
			{
				responce.Description = "Статистика не найдена";
				return responce;
			}

			responce.Data = new StatisticViewModel
			{
				//TODO:Сделать маппинг данных
				Id = statistic.Id
				//User = statistic.User,
			};

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<StatisticViewModel>
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
			var statistic = await _statisticRepository.Get(id);
			if (statistic == null)
			{
				responce.Description = $"Статистика по ID {id} не найдена";
				return responce;
			}

			responce.Data = await _statisticRepository.DeleteAsync(statistic);
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