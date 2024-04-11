using KingMeetup.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingMeetup.Repository.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(50);

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
