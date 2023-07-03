using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityTypeConfigurations;

public class EnergyConfiguration : IEntityTypeConfiguration<Energy>
{
	public void Configure(EntityTypeBuilder<Energy> builder)
	{
		builder
			.HasKey(k => k.Id);
	}
}