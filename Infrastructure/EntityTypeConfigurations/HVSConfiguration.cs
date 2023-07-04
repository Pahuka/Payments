using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityTypeConfigurations;

public class HVSConfiguration : IEntityTypeConfiguration<HVS>
{
	public void Configure(EntityTypeBuilder<HVS> builder)
	{
		builder
			.HasKey(k => k.Id);
		builder
			.Property(p => p.Id).HasConversion(new GuidToStringConverter());
	}
}