using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityTypeConfigurations;

public class HVSConfiguration : IEntityTypeConfiguration<HVS>
{
	public void Configure(EntityTypeBuilder<HVS> builder)
	{
		builder
			.HasKey(k => k.Id);
	}
}