using Application.Responce;

namespace Infrastructure.Services.Interfaces;

public interface IServiceBase<T>
{
	Task<IResponce<IEnumerable<T>>> GetAll();
	Task<IResponce<bool>> Create(T viewModel);
}