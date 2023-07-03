using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HVSConfiguration : IEntityTypeConfiguration<HVS>
{
	public void Configure(EntityTypeBuilder<HVS> builder)
	{
		builder
			.HasKey(k => k.Id);
	}
}