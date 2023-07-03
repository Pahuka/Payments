using Domain.Entities.Abstract;

namespace Domain.Entities;

public class User : EntityBase
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public IEnumerable<Statistic> Statistic { get; set; }
	public bool HasHvsMeter { get; set; }
	public bool HasGvsMeter { get; set; }
	public bool HasEnergyMeter { get; set; }
}