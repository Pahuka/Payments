using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnergyRepository : IEnergyRepository
{
	private readonly IAppDbContext _appDbContext;

	public EnergyRepository(IAppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(Energy entity)
	{
		await _appDbContext.Energy.AddAsync(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<IQueryable<Energy>> GetAll()
	{
		return _appDbContext.Energy.AsQueryable().Include(x => x.User);
	}

	public async Task<bool> DeleteAsync(Energy entity)
	{
		_appDbContext.Energy.Remove(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<Energy> Update(Energy entity)
	{
		_appDbContext.Energy.Update(entity);
		await _appDbContext.SaveChangesAsync(CancellationToken.None);

		return entity;
	}

	public async Task<Energy> Get(Guid Id)
	{
		return await _appDbContext.Energy
			.Include(x => x.User)
			.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}
}