using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.HasKey(k => k.Id);
		builder
			.Property(p => p.Id).HasConversion(new GuidToStringConverter());
		builder
			.HasMany(d => d.EnergyStatistic)
			.WithOne(p => p.User)
			.HasForeignKey(k => k.UserId);
		builder
			.HasMany(d => d.GVSStatistic)
			.WithOne(p => p.User)
			.HasForeignKey(k => k.UserId);
		builder
			.HasMany(d => d.HVSStatistic)
			.WithOne(p => p.User)
			.HasForeignKey(k => k.UserId);
	}
}