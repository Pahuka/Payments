using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityTypeConfigurations;

public class EnergyMeterConfiguration : IEntityTypeConfiguration<EnergyMeter>
{
	public void Configure(EntityTypeBuilder<EnergyMeter> builder)
	{
		builder
			.HasKey(k => k.Id);
	}
}