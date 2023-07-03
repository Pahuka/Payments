using Domain.Entities;

namespace Infrastructure.ViewModels;

public class EnergyMeterViewModel : ViewModelBase
{
	public EnergyMeterViewModel()
	{
		
	}

	public EnergyMeterViewModel(EnergyMeter energyMeter)
	{
		Id = energyMeter.Id;
		CurrentValueDay = energyMeter.CurrentValueDay;
		CurrentValueNight = energyMeter.CurrentValueNight;
	}

	public double CurrentValueNight { get; set; }
	public double CurrentValueDay { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}