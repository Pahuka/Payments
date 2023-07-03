namespace Infrastructure.ViewModels;

public class HVSViewModel : ViewModelBase
{
	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}