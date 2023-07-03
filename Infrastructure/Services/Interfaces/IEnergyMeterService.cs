using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IEnergyMeterService : IServiceBase<EnergyMeterViewModel>
{
	Task<IResponce<EnergyMeterViewModel>> GetById(Guid id);
	Task<IResponce<bool>> Delete(Guid id);
}