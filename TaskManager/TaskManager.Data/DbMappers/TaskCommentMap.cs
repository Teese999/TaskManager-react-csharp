using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Data.Models;

namespace TaskManager.Data.DbMappers
{
	public class TaskCommentMap : IEntityTypeConfiguration<TaskComment>
	{
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            builder.ToTable("TaskComments");
            builder.Property(x => x.CommentType).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.TaskId);
            builder.Property(x => x.FileName);


        }
    }
}

