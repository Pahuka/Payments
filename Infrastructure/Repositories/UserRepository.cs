using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly IAppDbContext _appDbContext;

	public UserRepository(IAppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<bool> Create(User entity)
	{
		await _appDbContext.Users.AddAsync(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<IQueryable<User>> GetAll()
	{
		return _appDbContext.Users.AsQueryable()
			.Include(x => x.GVSStatistic)
			.Include(x => x.EnergyStatistic)
			.Include(x => x.HVSStatistic);
	}

	public async Task<bool> DeleteAsync(User entity)
	{
		_appDbContext.Users.Remove(entity);
		return _appDbContext.SaveChangesAsync(CancellationToken.None).IsCompletedSuccessfully;
	}

	public async Task<User> Update(User entity)
	{
		_appDbContext.Users.Update(entity);
		await _appDbContext.SaveChangesAsync(CancellationToken.None);

		return entity;
	}

	public async Task<User> Get(string login)
	{
		return await _appDbContext.Users
			.Include(x => x.GVSStatistic)
			.Include(x => x.EnergyStatistic)
			.Include(x => x.HVSStatistic)
			.FirstOrDefaultAsync(x => x.Login.Equals(login));
	}

	public async Task<User> GetById(Guid id)
	{
		return await _appDbContext.Users
			.Include(x => x.GVSStatistic)
			.Include(x => x.EnergyStatistic)
			.Include(x => x.HVSStatistic)
			.FirstOrDefaultAsync(x => x.Login.Equals(id));
	}
}