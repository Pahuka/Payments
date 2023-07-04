using Domain.Entities;

namespace Infrastructure.ViewModels;

public class StatisticViewModel : ViewModelBase
{
	public StatisticViewModel()
	{
		
	}

	public StatisticViewModel(Statistic statistic)
	{
		Id = statistic.Id;
		HVS = new HVSViewModel(statistic?.HVS){User = this};
		GVS = new GVSViewModel(statistic?.GVS) {User = this};
		Energy = new EnergyViewModel(statistic?.Energy) { User = this };
		EnergyMeter = new EnergyMeterViewModel(statistic?.EnergyMeter) { Statistic = this };
	}

	public UserViewModel? User { get; set; }
	public HVSViewModel? HVS { get; set; }
	public GVSViewModel? GVS { get; set; }
	public EnergyViewModel? Energy { get; set; }
	public EnergyMeterViewModel? EnergyMeter { get; set; }
}