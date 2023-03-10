using System;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Data.Models;

namespace TaskManager.Data.DbMappers
{
	public class ProjectTaskMap : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.ToTable("Tasks");
            builder.Property(x => x.TaskName).HasMaxLength(255).IsRequired();
            builder.Property(x => x.CreateDate).HasDefaultValue(DateTime.MinValue);
            builder.Property(x => x.UpdateTime).HasDefaultValue(DateTime.MinValue);
            builder.Property(x => x.StartDate).HasDefaultValue(DateTime.MinValue);
            builder.Property(x => x.CancelDate).HasDefaultValue(DateTime.MinValue);
            builder.HasMany(p => p.TaskComments).WithOne().HasForeignKey(c => c.TaskId);

        }
    }
}

