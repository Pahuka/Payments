using Application.Responce;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Interfaces;

public interface IGVSService : IServiceBase<GVSViewModel>
{
	Task<IResponce<GVSViewModel>> GetById(Guid id);
	Task<IResponce<bool>> Delete(Guid id);
}