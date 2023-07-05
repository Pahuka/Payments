using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.ViewModels;

public class UserViewModel : ViewModelBase
{
	public UserViewModel()
	{
		EnergyStatistic = new List<EnergyViewModel>();
		GVSStatistic = new List<GVSViewModel>();
		HVSStatistic = new List<HVSViewModel>();
	}
	public UserViewModel(User user)
	{
		Id = user.Id;
		Date = user.CreatedDate;
		FirstName = user.FirstName;
		LastName = user.LastName;
		Login = user.Login;
		Password = user.Password;
		HasEnergyMeter = user.HasEnergyMeter;
		HasGvsMeter = user.HasGvsMeter;
		HasHvsMeter = user.HasHvsMeter;
		PeopleCount = user.PeopleCount;
		EnergyStatistic = new List<EnergyViewModel>();
		GVSStatistic = new List<GVSViewModel>();
		HVSStatistic = new List<HVSViewModel>();
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public IList<EnergyViewModel> EnergyStatistic { get; set; }
	public IList<GVSViewModel> GVSStatistic { get; set; }
	public IList<HVSViewModel> HVSStatistic { get; set; }
	public int PeopleCount { get; set; }
	public bool HasHvsMeter { get; set; }
	public bool HasGvsMeter { get; set; }
	public bool HasEnergyMeter { get; set; }

	public double GetTotalResult(IEnumerable<double> statCount)
	{
		return Math.Round(statCount.Sum(), 2);
	}
}