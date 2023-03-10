using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Data.Models;

namespace TaskManager.Data.DbMappers
{
	public class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.Property(x => x.CreateDate).IsRequired();
            builder.Property(x => x.UpdateTime).IsRequired();
            builder.HasMany(p => p.Tasks).WithOne().HasForeignKey(c => c.ProjectId);

        }
    }
}

