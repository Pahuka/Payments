using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IAppDbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<GVS> GVS { get; set; }
	public DbSet<HVS> HVS { get; set; }

	public DbSet<Energy> Energy { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken); //TODO: Возможно придется убрать
}