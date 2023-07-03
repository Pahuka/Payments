using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder
			.HasMany(d => d.Statistic)
			.WithOne(p => p.User)
			.HasForeignKey(d => d.UserId);
	}
}