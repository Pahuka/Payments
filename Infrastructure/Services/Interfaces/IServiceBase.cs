using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IServiceBase<T>
{
	Task<IResponce<IList<T>>> GetAll();
	Task<IResponce<bool>> Create(T viewModel);
	Task<IResponce<T>> Edit(T viewModel);
}