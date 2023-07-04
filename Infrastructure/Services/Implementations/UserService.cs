using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class UserService : IUserService
{
	private readonly IEnergyRepository _energyRepository;
	private readonly IGVSRepository _gvsRepository;
	private readonly IHVSRepository _hvsRepository;
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository, IEnergyRepository energyRepository, IGVSRepository gvsRepository,
		IHVSRepository hvsRepository)
	{
		_userRepository = userRepository;
		_energyRepository = energyRepository;
		_gvsRepository = gvsRepository;
		_hvsRepository = hvsRepository;
	}

	public async Task<IResponce<IList<UserViewModel>>> GetAll()
	{
		var responce = new Responce<IList<UserViewModel>> { Data = new List<UserViewModel>() };
		try
		{
			var users = _userRepository.GetAll().Result;

			if (users.Count() == 0)
			{
				responce.Description = "Найдено 0 пользователей";
				return responce;
			}

			foreach (var user in users)
			{
				responce.Data.Add(new UserViewModel(user));

				if (user.EnergyStatistic != null)
				{
					var userVm = responce.Data.Last();
					userVm.EnergyStatistic = new List<EnergyViewModel>();

					foreach (var statistic in user.EnergyStatistic)
						responce.Data.Last().EnergyStatistic.Add(new EnergyViewModel(statistic) { User = userVm });
				}

				if (user.GVSStatistic != null)
				{
					var userVm = responce.Data.Last();
					userVm.GVSStatistic = new List<GVSViewModel>();

					foreach (var statistic in user.GVSStatistic)
						responce.Data.Last().GVSStatistic.Add(new GVSViewModel(statistic) { User = userVm });
				}

				if (user.HVSStatistic != null)
				{
					var userVm = responce.Data.Last();
					userVm.HVSStatistic = new List<HVSViewModel>();

					foreach (var statistic in user.HVSStatistic)
						responce.Data.Last().HVSStatistic.Add(new HVSViewModel(statistic) { User = userVm });
				}
			}

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IList<UserViewModel>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(UserViewModel viewModel)
	{
		var responce = new Responce<bool>();
		try
		{
			var user = new User
			{
				FirstName = viewModel.FirstName,
				LastName = viewModel.LastName,
				Login = viewModel.Login,
				Password = viewModel.Password,
				HasEnergyMeter = viewModel.HasEnergyMeter,
				HasHvsMeter = viewModel.HasHvsMeter,
				HasGvsMeter = viewModel.HasGvsMeter
			};

			user.EnergyStatistic = await ViewModelEnergyStatistic(viewModel?.EnergyStatistic, user);
			user.HVSStatistic = await ViewModelHVSStatistic(viewModel?.HVSStatistic, user);
			user.GVSStatistic = await ViewModelGVSStatistic(viewModel?.GVSStatistic, user);

			responce.Data = await _userRepository.Create(user);

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

	public async Task<IResponce<UserViewModel>> GetByLogin(string login)
	{
		var responce = new Responce<UserViewModel>();
		try
		{
			var user = await _userRepository.Get(login);

			if (user == null)
			{
				responce.Description = "Найдено 0 пользователей";
				return responce;
			}

			responce.Data = new UserViewModel(user);
			responce.Data.HVSStatistic =
				user.HVSStatistic.Select(x => new HVSViewModel(x) { User = responce.Data }).ToList();
			responce.Data.EnergyStatistic = 
				user.EnergyStatistic.Select(x => new EnergyViewModel(x) { User = responce.Data }).ToList();
			responce.Data.GVSStatistic =
				user.GVSStatistic.Select(x => new GVSViewModel(x) { User = responce.Data }).ToList();

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<UserViewModel>
			{
				Description = $"[GetByLogin] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<UserViewModel>> GetById(Guid id)
	{
		var responce = new Responce<UserViewModel>();
		try
		{
			var user = await _userRepository.GetById(id);

			if (user == null)
			{
				responce.Description = "Найдено 0 пользователей";
				return responce;
			}

			responce.Data = new UserViewModel(user);
			responce.Data.HVSStatistic =
				user.HVSStatistic.Select(x => new HVSViewModel(x) { User = responce.Data }).ToList();
			responce.Data.EnergyStatistic =
				user.EnergyStatistic.Select(x => new EnergyViewModel(x) { User = responce.Data }).ToList();
			responce.Data.GVSStatistic =
				user.GVSStatistic.Select(x => new GVSViewModel(x) { User = responce.Data }).ToList();

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<UserViewModel>
			{
				Description = $"[GetByLogin] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<UserViewModel>> Edit(UserViewModel viewModel)
	{
		var responce = new Responce<UserViewModel>();
		try
		{
			var user = await _userRepository.Get(viewModel.Login);

			if (user == null)
			{
				responce.Description = "Найдено 0 пользователей";
				return responce;
			}

			user.FirstName = viewModel.FirstName;
			user.LastName = viewModel.LastName;
			user.Login = viewModel.Login;
			user.Password = viewModel.Password;
			user.EnergyStatistic = await ViewModelEnergyStatistic(viewModel.EnergyStatistic, user);
			user.HVSStatistic = await ViewModelHVSStatistic(viewModel.HVSStatistic, user);
			user.GVSStatistic = await ViewModelGVSStatistic(viewModel.GVSStatistic, user);

			await _userRepository.Update(user);
			responce.Data = viewModel;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<UserViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Delete(string login)
	{
		var responce = new Responce<bool>();
		try
		{
			var user = await _userRepository.Get(login);
			if (user == null)
			{
				responce.Description = $"Пользователь с логином {login} не найден";
				return responce;
			}

			responce.Data = await _userRepository.DeleteAsync(user);
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

	private async Task<IList<Energy>> ViewModelEnergyStatistic(IList<EnergyViewModel> viewModel, User user)
	{
		if (viewModel == null)
			return null;

		var result = new List<Energy>();

		foreach (var energyView in viewModel)
		{
			var energy = new Energy
			{
				DayValue = energyView.DayValue,
				NightValue = energyView.NightValue,
				NormativValue = energyView.NormativValue,
				User = user
			};

			await _energyRepository.Create(energy);

			result.Add(energy);
		}

		return result;
	}

	private async Task<IList<GVS>> ViewModelGVSStatistic(IList<GVSViewModel> viewModel, User user)
	{
		if (viewModel == null)
			return null;

		var result = new List<GVS>();

		foreach (var gvsViewModel in viewModel)
		{
			var gvs = new GVS
			{
				CurrentValue = gvsViewModel.CurrentValue,
				User = user
			};

			await _gvsRepository.Create(gvs);

			result.Add(gvs);
		}

		return result;
	}

	private async Task<IList<HVS>> ViewModelHVSStatistic(IList<HVSViewModel> viewModel, User user)
	{
		if (viewModel == null)
			return null;

		var result = new List<HVS>();

		foreach (var hvsViewModel in viewModel)
		{
			var hvs = new HVS
			{
				CurrentValue = hvsViewModel.CurrentValue,
				User = user
			};

			await _hvsRepository.Create(hvs);

			result.Add(hvs);
		}

		return result;
	}
}