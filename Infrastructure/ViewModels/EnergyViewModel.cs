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
		CurrentValue = energy.CurrentValue;
	}

	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}