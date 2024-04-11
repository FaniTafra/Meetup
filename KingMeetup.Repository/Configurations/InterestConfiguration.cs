using KingMeetup.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingMeetup.Repository.Configurations
{
    public class InterestConfiguration : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

			builder.HasMany(x=>x.UsersInterests)
					.WithOne(x=>x.Interest)
					.HasForeignKey(x => x.InterestId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x=>x.Events)
					.WithOne(x => x.Interest)
					.HasForeignKey(x => x.InterestId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			#region AuditableEntity
			builder.Property(x => x.DateCreated)
				.HasColumnType("datetime")
				.HasDefaultValueSql("(getutcdate())");

			builder.Property(x => x.CreatedBy)
							.HasMaxLength(64)
							.IsUnicode();

			builder.Property(x => x.DateModified)
				.HasColumnType("datetime")
				.HasDefaultValueSql("(getutcdate())");

			builder.Property(x => x.ModifiedBy)
							.HasMaxLength(64)
							.IsUnicode();

			builder.Property(x => x.Active)
				.HasDefaultValueSql("((1))");
			#endregion
		}
	}
}
