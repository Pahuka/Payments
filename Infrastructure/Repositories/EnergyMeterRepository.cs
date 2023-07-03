using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EnergyMeterRepository : IEnergyMeterRepository
{
	private readonly IAppDbContext _appDbContext;

	public EnergyMeterRepository(IAppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(EnergyMeter entity)
	{
		await _appDbContext.EnergyMeter.AddAsync(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<IQueryable<EnergyMeter>> GetAll()
	{
		return _appDbContext.EnergyMeter.AsQueryable()
			.Include(x => x.Statistic);
	}

	public async Task<bool> DeleteAsync(EnergyMeter entity)
	{
		_appDbContext.EnergyMeter.Remove(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<EnergyMeter> Update(EnergyMeter entity)
	{
		_appDbContext.EnergyMeter.Update(entity);
		await _appDbContext.SaveChangesAsync(CancellationToken.None);

		return entity;
	}

	public async Task<EnergyMeter> Get(Guid Id)
	{
		return await _appDbContext.EnergyMeter
			.Include(x => x.Statistic)
			.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}
}