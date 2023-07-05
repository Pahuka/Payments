using Application.Interfaces;
using Application.Responce;
using Domain.Entities;
using Infrastructure.Services.Interfaces;
using Infrastructure.ViewModels;

namespace Infrastructure.Services.Implementations;

public class GVSService : IGVSService
{
	private readonly double _normativTN = 4.01;
	private readonly double _normativTE = 0.05349;
	private readonly double _tarifTE = 998.69;
	private readonly double _tarifTN = 35.78;
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
					User = new UserViewModel(gvs.User)
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
				UserId = viewModel.UserId
			};

			var user = await _userRepository.GetById(viewModel.UserId);
			responce.Data = await _gvsRepository.Create(await GetStatisticResult(gvs, user));

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
				User = new UserViewModel(gvs.User)
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

	private async Task<GVS> GetStatisticResult(GVS currentGVS, User user)
	{
		var lastGVS = user.GVSStatistic.LastOrDefault();
		var gvsTN = 0.0;

		if (lastGVS == null)
			return currentGVS;

		if (user.HasGvsMeter)
		{
			gvsTN = Math.Abs(currentGVS.CurrentValue - lastGVS.CurrentValue);
			
			currentGVS.TotalResultTN += gvsTN * _tarifTN;
			currentGVS.TotalResultTE += (gvsTN * _normativTE) * _tarifTE;
		}
		else
		{
			gvsTN = user.PeopleCount * _normativTN;

			currentGVS.TotalResultTE += gvsTN * _tarifTE;
			currentGVS.TotalResultTN += (gvsTN * _normativTE) * _tarifTE;
		}

		return currentGVS;
	}
}