using Domain.Entities;

namespace Infrastructure.ViewModels;

public class EnergyViewModel : ViewModelBase
{
	public EnergyViewModel()
	{
		
	}

	public EnergyViewModel(Energy energy)
	{
		Id = energy.Id;
		NormativValue = energy.NormativValue;
		DayValue = energy.DayValue;
		NightValue = energy.NightValue;
	}

	public double NormativValue { get; set; }
	public double DayValue { get; set; }
	public double NightValue { get; set; }
	public UserViewModel? User { get; set; }
}