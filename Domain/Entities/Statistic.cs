using Domain.Entities.Abstract;

namespace Domain.Entities;

public class Statistic : EntityBase
{
	public User? User { get; set; }
	public Guid UserId { get; set; }
	public HVS? HVS { get; set; }
	public Guid HVSId { get; set; }
	public GVS? GVS { get; set; }
	public Guid GVSId { get; set; }
	public Energy? Energy { get; set; }
	public Guid EnergyId { get; set; }
	public EnergyMeter? EnergyMeter { get; set; }
	public Guid EnergyMeterId { get; set; }
}