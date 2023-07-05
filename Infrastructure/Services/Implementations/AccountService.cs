using Application.Interfaces;
using Application.Responce;
using System.Security.Claims;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Implementations;

public class AccountService : IAccountService
{
	private readonly IUserRepository _userRepository;

	public AccountService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<Responce<ClaimsIdentity>> Register(UserViewModel model)
	{
		try
		{
			var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Login == model.Login);
			if (user != null)
				return new Responce<ClaimsIdentity>
				{
					Description = "Пользователь с таким логином уже есть"
				};

			user = new User()
			{
				Login = model.Login,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Password = model.Password,
				PeopleCount = model.PeopleCount,
				HasEnergyMeter = model.HasEnergyMeter,
				HasGvsMeter = model.HasGvsMeter,
				HasHvsMeter = model.HasHvsMeter
			};

			await _userRepository.Create(user);

			var result = Authenticate(user);

			return new Responce<ClaimsIdentity>
			{
				Data = result,
				Description = "Пользователь зарегистрирован",
			};
		}
		catch (Exception ex)
		{
			return new Responce<ClaimsIdentity>
			{
				Description = ex.Message
			};
		}
	}

	public async Task<Responce<ClaimsIdentity>> Login(UserViewModel model)
	{
		try
		{
			var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Login == model.Login);
			if (user == null)
				return new Responce<ClaimsIdentity>
				{
					Description = "Пользователь не найден"
				};

			if (user.Password != model.Password)
				return new Responce<ClaimsIdentity>
				{
					Description = "Неверный пароль или логин"
				};
			var result = Authenticate(user);

			return new Responce<ClaimsIdentity>
			{
				Data = result
			};
		}
		catch (Exception ex)
		{
			return new Responce<ClaimsIdentity>
			{
				Description = ex.Message
			};
		}
	}

	public async Task<Responce<bool>> ChangePassword(UserViewModel model)
	{
		try
		{
			var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Login == model.Login);
			if (user == null)
				return new Responce<bool>
				{
					Description = "Пользователь не найден"
				};

			//TODO:Возможно надо отдельную модель
			user.Password = model.Password;
			await _userRepository.Update(user);

			return new Responce<bool>
			{
				Data = true,
				Description = "Пароль обновлен"
			};
		}
		catch (Exception ex)
		{
			return new Responce<bool>
			{
				Description = ex.Message
			};
		}
	}

	private ClaimsIdentity Authenticate(User user)
	{
		//var role = user.IsAdministrator ? "Administrator" : "User";
		var role = "User";
		var claims = new List<Claim>
		{
			new(ClaimsIdentity.DefaultNameClaimType, user.Login),
			new(ClaimsIdentity.DefaultRoleClaimType, role)
		};
		return new ClaimsIdentity(claims, "ApplicationCookie",
			ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
	}
}