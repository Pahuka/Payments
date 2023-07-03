namespace Infrastructure.ViewModels;

public class GVSViewModel : ViewModelBase
{
	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}