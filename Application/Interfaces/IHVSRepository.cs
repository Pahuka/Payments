using Domain.Entities;

namespace Application.Interfaces;

public interface IHVSRepository : IRepository<HVS>
{
	Task<HVS> Get(Guid Id);
}