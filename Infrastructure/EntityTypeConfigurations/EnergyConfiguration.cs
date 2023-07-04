using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityTypeConfigurations;

public class EnergyConfiguration : IEntityTypeConfiguration<Energy>
{
	public void Configure(EntityTypeBuilder<Energy> builder)
	{
		builder
			.HasKey(k => k.Id);
		builder
			.Property(p => p.Id).HasConversion(new GuidToStringConverter());
	}
}