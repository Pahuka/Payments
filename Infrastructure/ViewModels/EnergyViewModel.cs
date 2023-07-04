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
		Date = energy.CreatedDate;
		NormativValue = energy.NormativValue;
		DayValue = energy.DayValue;
		NightValue = energy.NightValue;
		UserId = energy.UserId;
		TotalResult = energy.TotalResult;
	}

	public double NormativValue { get; set; }
	public double DayValue { get; set; }
	public double NightValue { get; set; }
	public double TotalResult { get; set; }
	public UserViewModel? User { get; set; }
	public Guid UserId { get; set; }
}