using Infrastructure.Data.Postgres.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Postgres.EntityFramework.Configurations;

public abstract class BaseConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
	where TEntity : Entity<TId>
{
	public virtual void Configure(EntityTypeBuilder<TEntity> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.CreatedAt).IsRequired();
		builder.Property(x => x.IsDeleted).IsRequired();
	}
}
