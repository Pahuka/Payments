using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IHVSService : IServiceBase<HVSViewModel>
{
	Task<IResponce<HVSViewModel>> GetById(Guid id);
	Task<IResponce<bool>> Delete(Guid id);
}