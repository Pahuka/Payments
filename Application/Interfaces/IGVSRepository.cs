using Domain.Entities;

namespace Application.Interfaces;

public interface IGVSRepository : IRepository<GVS>
{
	Task<GVS> Get(Guid Id);
}