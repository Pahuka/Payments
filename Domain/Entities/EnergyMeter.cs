using Domain.Entities.Abstract;

namespace Domain.Entities;

public class EnergyMeter : EntityBase
{
    public double CurrentValueNight { get; set; }
    public double CurrentValueDay { get; set; }
    public Statistic? Statistic { get; set; }
}