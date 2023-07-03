using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IUserService : IServiceBase<UserViewModel>
{
	Task<IResponce<UserViewModel>> GetByLogin(string login);
	Task<IResponce<UserViewModel>> Edit(UserViewModel viewModel);

	Task<IResponce<bool>> Delete(string login);
}