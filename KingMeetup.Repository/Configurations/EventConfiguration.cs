using KingMeetup.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingMeetup.Repository.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

			builder.HasMany(x=>x.EventReviews)
					.WithOne(x=>x.Event)
					.HasForeignKey(x => x.EventId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x=>x.Certificates)
					.WithOne(x => x.Event)
					.HasForeignKey(x => x.EventId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x=>x.Notifications)
					.WithOne(x => x.Event)
					.HasForeignKey(x => x.EventId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x=>x.AttendeeLists)
					.WithOne(x => x.Event)
					.HasForeignKey(x=>x.EventId)
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
