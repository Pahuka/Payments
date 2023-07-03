using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<IResponce<IEnumerable<UserViewModel>>> GetAll()
	{
		var responce = new Responce<IEnumerable<UserViewModel>>(){Data = new List<UserViewModel>()};
		try
		{
			var users = _userRepository.GetAll().Result;

			if (users.Count() == 0)
			{
				responce.Description = "Найдено 0 пользователей";
				return responce;
			}

			foreach (var user in users)
				responce.Data.Append(new UserViewModel
				{
					FirstName = user.FirstName,
					LastName = user.LastName,
					Login = user.Login,
					Password = user.Password,
					HasEnergyMeter = user.HasEnergyMeter,
					HasGvsMeter = user.HasGvsMeter,
					HasHvsMeter = user.HasHvsMeter,
					Id = user.Id
					//Statistic 
				});

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IEnumerable<UserViewModel>>
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

	public Task<IResponce<UserViewModel>> GetByLogin(string login)
	{
		throw new NotImplementedException();
	}

	public Task<IResponce<UserViewModel>> Edit(UserViewModel viewModel)
	{
		throw new NotImplementedException();
	}

	public Task<IResponce<bool>> Delete(string login)
	{
		throw new NotImplementedException();
	}
}