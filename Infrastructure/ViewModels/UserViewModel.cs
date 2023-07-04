using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.ViewModels;

public class UserViewModel : ViewModelBase
{
	public UserViewModel()
	{
		
	}
	public UserViewModel(User user)
	{
		Id = user.Id;
		FirstName = user.FirstName;
		LastName = user.LastName;
		Login = user.Login;
		Password = user.Password;
		HasEnergyMeter = user.HasEnergyMeter;
		HasGvsMeter = user.HasGvsMeter;
		HasHvsMeter = user.HasHvsMeter;
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public IList<EnergyViewModel> EnergyStatistic { get; set; }
	public IList<GVSViewModel> GVSStatistic { get; set; }
	public IList<HVSViewModel> HVSStatistic { get; set; }
	public bool HasHvsMeter { get; set; }
	public bool HasGvsMeter { get; set; }
	public bool HasEnergyMeter { get; set; }
}