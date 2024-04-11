﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KingMeetup.Model;

namespace KingMeetup.Repository.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

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
