using KingMeetup.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KingMeetup.Repository.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Phone)
                .HasMaxLength(64);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.HasMany(x=>x.AttendeeLists)
                    .WithOne(x=>x.User)
					.HasForeignKey(x => x.UserId)
					.IsRequired()
                    .OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x => x.UsersInterests)
					.WithOne(x => x.User)
					.HasForeignKey(x => x.UserId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x => x.Certificates)
					.WithOne(x => x.User)
					.HasForeignKey(x => x.UserId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x => x.Events)
					.WithOne(x => x.User)
					.HasForeignKey(x => x.UserId)
					.IsRequired()
					.OnDelete(DeleteBehavior.ClientSetNull);

			builder.HasMany(x => x.EventReviews)
					.WithOne(x => x.User)
					.HasForeignKey(x => x.UserId)
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
