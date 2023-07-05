using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GVSRepository : IGVSRepository
{
	private readonly IAppDbContext _appDbContext;

	public GVSRepository(IAppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(GVS entity)
	{
		await _appDbContext.GVS.AddAsync(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<IQueryable<GVS>> GetAll()
	{
		return _appDbContext.GVS.AsQueryable()
			.Include(x => x.User);
	}

	public async Task<bool> DeleteAsync(GVS entity)
	{
		_appDbContext.GVS.Remove(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<GVS> Update(GVS entity)
	{
		_appDbContext.GVS.Update(entity);
		await _appDbContext.SaveChangesAsync(CancellationToken.None);

		return entity;
	}

	public async Task<GVS> Get(Guid Id)
	{
		return await _appDbContext.GVS
			.Include(x => x.User)
			.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}
}