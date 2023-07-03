using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class StatisticRepository : IStatisticRepository
{
	private readonly IAppDbContext _appDbContext;

	public StatisticRepository(IAppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(Statistic entity)
	{
		await _appDbContext.Statistics.AddAsync(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<IQueryable<Statistic>> GetAll()
	{
		return _appDbContext.Statistics.AsQueryable()
			.Include(x => x.User)
			.Include(x => x.Energy)
			.Include(x => x.EnergyMeter)
			.Include(x => x.GVS)
			.Include(x => x.HVS);
	}

	public async Task<bool> DeleteAsync(Statistic entity)
	{
		_appDbContext.Statistics.Remove(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<Statistic> Update(Statistic entity)
	{
		_appDbContext.Statistics.Update(entity);
		await _appDbContext.SaveChangesAsync(CancellationToken.None);

		return entity;
	}

	public async Task<Statistic> Get(Guid Id)
	{
		return await _appDbContext.Statistics
			.Include(x => x.User)
			.Include(x => x.Energy)
			.Include(x => x.EnergyMeter)
			.Include(x => x.GVS)
			.Include(x => x.HVS)
			.FirstOrDefaultAsync(x => x.Id.Equals(Id));
	}
}