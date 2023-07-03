using Domain.Entities;

namespace Application.Interfaces;

public interface IEnergyRepository : IRepository<Energy>
{
	Task<Energy> Get(Guid Id);
}