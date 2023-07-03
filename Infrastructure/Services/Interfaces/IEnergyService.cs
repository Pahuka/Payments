using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IEnergyService : IServiceBase<EnergyViewModel>
{
	Task<IResponce<EnergyViewModel>> GetById(Guid id);
	Task<IResponce<bool>> Delete(Guid id);
}