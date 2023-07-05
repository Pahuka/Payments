using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Energy : EntityBase
{
	public double DayValue { get; set; }
	public double NightValue { get; set; }
	public double TotalResult { get; set; }
	public User? User { get; set; }
	public Guid UserId { get; set; }
}