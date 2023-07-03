using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IStatisticService : IServiceBase<StatisticViewModel>
{
	Task<IResponce<StatisticViewModel>> GetById (Guid id);
	Task<IResponce<bool>> Delete(Guid id);
}