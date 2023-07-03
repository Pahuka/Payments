using Domain.Entities;

namespace Application.Interfaces;

public interface IEnergyMeterRepository : IRepository<EnergyMeter>
{
	Task<EnergyMeter> Get(Guid Id);
}