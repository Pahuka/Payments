using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class StatisticConfiguration : IEntityTypeConfiguration<Statistic>
{
	public void Configure(EntityTypeBuilder<Statistic> builder)
	{
		builder
			.HasKey(k => k.Id);
		builder
			.HasOne(d => d.Energy)
			.WithOne(p => p.User);
		builder
			.HasOne(d => d.GVS)
			.WithOne(p => p.User);
		builder
			.HasOne(d => d.HVS)
			.WithOne(p => p.User);
		builder
			.HasOne(d => d.EnergyMeter)
			.WithOne(p => p.Statistic);
	}
}