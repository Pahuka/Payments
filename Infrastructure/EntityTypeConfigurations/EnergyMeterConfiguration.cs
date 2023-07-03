using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class EnergyMeterConfiguration : IEntityTypeConfiguration<EnergyMeter>
{
	public void Configure(EntityTypeBuilder<EnergyMeter> builder)
	{
		builder
			.HasKey(k => k.Id);
	}
}