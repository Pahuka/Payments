using Domain.Entities;

namespace Application.Interfaces;

public interface IStatisticRepository : IRepository<Statistic>
{
	Task<Statistic> Get(Guid Id);
}