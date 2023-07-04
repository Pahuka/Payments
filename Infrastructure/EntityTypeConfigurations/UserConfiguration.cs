using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.HasMany(d => d.EnergyStatistic)
			.WithOne(p => p.User)
			.HasForeignKey(d => d.UserId);
		builder
			.HasMany(d => d.GVSStatistic)
			.WithOne(p => p.User)
			.HasForeignKey(d => d.UserId);
		builder
			.HasMany(d => d.HVSStatistic)
			.WithOne(p => p.User)
			.HasForeignKey(d => d.UserId);
	}
}