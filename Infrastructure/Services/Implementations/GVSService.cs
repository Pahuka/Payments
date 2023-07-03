using Application.Interfaces;
using Application.Responce;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class GVSService : IGVSService
{
	private readonly IGVSRepository _gvsRepository;

	public GVSService(IGVSRepository gvsRepository)
	{
		_gvsRepository = gvsRepository;
	}

	public async Task<IResponce<IList<GVSViewModel>>> GetAll()
	{
		var responce = new Responce<IList<GVSViewModel>>() { Data = new List<GVSViewModel>() };
		try
		{
			var gvses = _gvsRepository.GetAll().Result;

			if (gvses.Count() == 0)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			foreach (var gvs in gvses)
				responce.Data.Add(new GVSViewModel
				{
					Id = gvs.Id,
					//Statistic = new StatisticService().GetById()
				});

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<IList<GVSViewModel>>
			{
				Description = $"[GetAll] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Create(GVSViewModel viewModel)
	{
		throw new NotImplementedException();
	}

	public async Task<IResponce<GVSViewModel>> Edit(GVSViewModel viewModel)
	{
		throw new NotImplementedException();
	}

	public async Task<IResponce<GVSViewModel>> GetById(Guid id)
	{
		throw new NotImplementedException();
	}

	public async Task<IResponce<bool>> Delete(Guid id)
	{
		throw new NotImplementedException();
	}
}