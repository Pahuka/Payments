namespace Infrastructure.ViewModels;

public class EnergyMeterViewModel : ViewModelBase
{
	public double CurrentValueNight { get; set; }
	public double CurrentValueDay { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}