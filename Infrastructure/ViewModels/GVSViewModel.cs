using Domain.Entities;

namespace Infrastructure.ViewModels;

public class GVSViewModel : ViewModelBase
{
	public GVSViewModel()
	{
		
	}
	public GVSViewModel(GVS gvs)
	{
		Id = gvs.Id;
		CurrentValue = gvs.CurrentValue;
	}

	public double CurrentValue { get; set; }
	public StatisticViewModel? Statistic { get; set; }
}