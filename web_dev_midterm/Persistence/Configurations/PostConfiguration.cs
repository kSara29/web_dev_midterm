﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web_dev_midterm.Models;

namespace web_dev_midterm.Persistence.Configurations;

public class PostConfiguration: IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id);
        builder.Property(p => p.UserId);
        builder.Property(p => p.Description);
        
    }
}
