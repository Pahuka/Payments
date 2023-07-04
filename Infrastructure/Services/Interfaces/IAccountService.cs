using Application.Responce;
using System.Security.Claims;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IAccountService
{
	Task<Responce<ClaimsIdentity>> Register(UserViewModel model);

	Task<Responce<ClaimsIdentity>> Login(UserViewModel model);

	Task<Responce<bool>> ChangePassword(UserViewModel model);
}