namespace Infrastructure.ViewModels;

public class StatisticViewModel : ViewModelBase
{
	public UserViewModel? User { get; set; }
	public HVSViewModel? HVS { get; set; }
	public GVSViewModel? GVS { get; set; }
	public EnergyViewModel? Energy { get; set; }
	public EnergyMeterViewModel? EnergyMeter { get; set; }
}