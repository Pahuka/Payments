using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.EntityTypeConfigurations;

public class GVSConfiguration : IEntityTypeConfiguration<GVS>
{
	public void Configure(EntityTypeBuilder<GVS> builder)
	{
		builder
			.HasKey(k => k.Id);
		builder
			.Property(p => p.Id).HasConversion(new GuidToStringConverter());
	}
}