using Domain.Entities;

namespace Infrastructure.ViewModels;

public class StatisticViewModel : ViewModelBase
{
	public StatisticViewModel()
	{
		
	}

	public StatisticViewModel(Statistic statistic)
	{
		HVS = new HVSViewModel(statistic?.HVS){Statistic = this};
		GVS = new GVSViewModel(statistic?.GVS) {Statistic = this};
		Energy = new EnergyViewModel(statistic?.Energy) { Statistic = this };
		EnergyMeter = new EnergyMeterViewModel(statistic?.EnergyMeter) { Statistic = this };
	}

	public UserViewModel? User { get; set; }
	public HVSViewModel? HVS { get; set; }
	public GVSViewModel? GVS { get; set; }
	public EnergyViewModel? Energy { get; set; }
	public EnergyMeterViewModel? EnergyMeter { get; set; }
}