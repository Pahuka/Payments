using Domain.Entities.Abstract;

namespace Domain.Entities;

public class HVS : EntityBase
{
	public double CurrentValue { get; set; }
	public Statistic? Statistic { get; set; }
}