using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web_dev_midterm.Models;

namespace web_dev_midterm.Persistence.Configurations;

public class SubscriptionConfiguration: IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id);
        builder.Property(s => s.TargetUserId);
        builder.Property(s => s.SubscriberId);
        
        builder
            .HasOne(s => s.Subscriber)
            .WithMany(u => u.Subscriptions)
            .HasForeignKey(s => s.SubscriberId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(s => s.TargetUser)
            .WithMany(u => u.Followers)
            .HasForeignKey(s => s.TargetUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
