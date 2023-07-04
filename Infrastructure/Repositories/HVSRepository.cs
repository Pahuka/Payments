using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HVSRepository : IHVSRepository
{
	private readonly IAppDbContext _appDbContext;

	public HVSRepository(IAppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(HVS entity)
	{
		await _appDbContext.HVS.AddAsync(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<IQueryable<HVS>> GetAll()
	{
		return _appDbContext.HVS.AsQueryable()
			.Include(x => x.User);
	}

	public async Task<bool> DeleteAsync(HVS entity)
	{
		_appDbContext.HVS.Remove(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<HVS> Update(HVS entity)
	{
		_appDbContext.HVS.Update(entity);
		await _appDbContext.SaveChangesAsync(CancellationToken.None);

		return entity;
	}

	public async Task<HVS> Get(Guid Id)
	{
		return await _appDbContext.HVS
			.Include(x => x.User)
			.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}
}