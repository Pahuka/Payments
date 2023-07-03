using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityTypeConfigurations;

public class GVSConfiguration : IEntityTypeConfiguration<GVS>
{
	public void Configure(EntityTypeBuilder<GVS> builder)
	{
		builder
			.HasKey(k => k.Id);
	}
}