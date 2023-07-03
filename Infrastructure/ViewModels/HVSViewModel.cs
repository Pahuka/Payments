using Domain.Entities;

namespace Infrastructure.ViewModels;

public class HVSViewModel : ViewModelBase
{
	public HVSViewModel()
	{
		
	}

	public HVSViewModel(HVS hvs)
	{
		Id = hvs.Id;
		CurrentValue = hvs.CurrentValue;
	}

	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}