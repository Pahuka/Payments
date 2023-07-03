namespace Infrastructure.ViewModels;

public class EnergyViewModel : ViewModelBase
{
	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}