using Domain.Entities;

namespace Infrastructure.ViewModels;

public class EnergyViewModel : ViewModelBase
{
	public EnergyViewModel()
	{
		
	}

	public EnergyViewModel(Energy energy)
	{
		CurrentValue = energy.CurrentValue;
	}

	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}