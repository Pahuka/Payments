using Application.Interfaces;
using Domain.Entities;
using Infrastructure.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<User> Users { get; set; }
	public DbSet<Statistic> Statistics { get; set; }
	public DbSet<GVS> GVS { get; set; }
	public DbSet<HVS> HVS { get; set; }
	public DbSet<Energy> Energy { get; set; }
	public DbSet<EnergyMeter> EnergyMeter { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new UserConfiguration());
		modelBuilder.ApplyConfiguration(new StatisticConfiguration());
		modelBuilder.ApplyConfiguration(new EnergyConfiguration());
		modelBuilder.ApplyConfiguration(new EnergyMeterConfiguration());
		modelBuilder.ApplyConfiguration(new HVSConfiguration());
		modelBuilder.ApplyConfiguration(new GVSConfiguration());
		base.OnModelCreating(modelBuilder);
	}
}