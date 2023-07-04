using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class GVSService : IGVSService
{
	private readonly IGVSRepository _gvsRepository;
	private readonly IUserRepository _userRepository;

	public GVSService(IGVSRepository gvsRepository, IUserRepository userRepository)
	{
		_gvsRepository = gvsRepository;
		_userRepository = userRepository;
	}

	public async Task<IResponce<IList<GVSViewModel>>> GetAll()
	{
		var responce = new Responce<IList<GVSViewModel>> { Data = new List<GVSViewModel>() };
		try
		{
			var gvses = _gvsRepository.GetAll().Result;

			if (gvses.Count() == 0)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			foreach (var gvs in gvses)
				responce.Data.Add(new GVSViewModel(gvs)
				{
					User = new UserViewModel(gvs?.User)
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
		var responce = new Responce<bool>();
		try
		{
			var gvs = new GVS
			{
				CurrentValue = viewModel.CurrentValue,
				User = await _userRepository.GetById(viewModel.User.Id)
			};

			responce.Data = await _gvsRepository.Create(gvs);

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<bool>
			{
				Description = $"[Create] : {e.Message}",
				Data = false
			};
		}
	}

	public async Task<IResponce<GVSViewModel>> Edit(GVSViewModel viewModel)
	{
		var responce = new Responce<GVSViewModel>();
		try
		{
			var gvs = await _gvsRepository.Get(viewModel.Id);

			if (gvs == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			gvs.CurrentValue = viewModel.CurrentValue;
			gvs.User = await _userRepository.GetById(viewModel.User.Id);

			await _gvsRepository.Update(gvs);
			responce.Data = viewModel;

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<GVSViewModel>
			{
				Description = $"[Edit] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<GVSViewModel>> GetById(Guid id)
	{
		var responce = new Responce<GVSViewModel>();
		try
		{
			var gvs = await _gvsRepository.Get(id);

			if (gvs == null)
			{
				responce.Description = "Показания не найдены";
				return responce;
			}

			responce.Data = new GVSViewModel(gvs)
			{
				User = new UserViewModel(gvs?.User)
			};

			return responce;
		}
		catch (Exception e)
		{
			return new Responce<GVSViewModel>
			{
				Description = $"[GetById] : {e.Message}"
			};
		}
	}

	public async Task<IResponce<bool>> Delete(Guid id)
	{
		var responce = new Responce<bool>();
		try
		{
			var gvs = await _gvsRepository.Get(id);
			if (gvs == null)
			{
				responce.Description = $"Показание с ID {id} не найдено";
				return responce;
			}

			responce.Data = await _gvsRepository.DeleteAsync(gvs);
			return responce;
		}
		catch (Exception e)
		{
			return new Responce<bool>
			{
				Description = $"[Delete] : {e.Message}",
				Data = false
			};
		}
	}
}