using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IStatisticRepository _statisticRepository;

	public UserService(IUserRepository userRepository, IStatisticRepository statisticRepository)
	{
		_userRepository = userRepository;
		_statisticRepository = statisticRepository;
	}

	public async Task<IResponce<IList<UserViewModel>>> GetAll()
	{
		var responce = new Responce<IList<UserViewModel>>(){Data = new List<UserViewModel>()};
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
				
				if (user.Statistic != null)
				{
					var userVm = responce.Data.Last();
					userVm.Statistic = new List<StatisticViewModel>();

					foreach (var statistic in user.Statistic)
					{
						responce.Data.Last().Statistic.Add(new StatisticViewModel(statistic) { User = userVm });
					}
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
			var user = new User()
			{
				FirstName = viewModel.FirstName,
				LastName = viewModel.LastName,
				Login = viewModel.Login,
				Password = viewModel.Password,
				HasEnergyMeter = viewModel.HasEnergyMeter,
				HasHvsMeter = viewModel.HasHvsMeter,
				HasGvsMeter = viewModel.HasGvsMeter
			};

			responce.Data = await _userRepository.Create(user);

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<bool>
			{
				Description = $"[Create] : {e.Message}",
				Data = false,
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
}