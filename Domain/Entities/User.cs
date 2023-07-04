using Domain.Entities.Abstract;

namespace Domain.Entities;

public class User : EntityBase
{
	public User()
	{
		EnergyStatistic = new List<Energy>();
		GVSStatistic = new List<GVS>();
		HVSStatistic = new List<HVS>();
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public IList<Energy> EnergyStatistic { get; set; }
	public IList<GVS> GVSStatistic { get; set; }
	public IList<HVS> HVSStatistic { get; set; }
	public bool HasHvsMeter { get; set; }
	public bool HasGvsMeter { get; set; }
	public bool HasEnergyMeter { get; set; }
}