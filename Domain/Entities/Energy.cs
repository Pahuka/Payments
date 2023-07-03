using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Energy : EntityBase
{
	public double CurrentValue { get; set; }
	public Statistic? Statistic { get; set; }
}