using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_dev_midterm.Models;

namespace web_dev_midterm.Persistence;

public class AppDbContext: IdentityDbContext<User>
{
    public DbSet<Post>? Posts { get; set; }
    public DbSet<PostComment>? PostComments { get; set; }
    public DbSet<Like>? Likes { get; set; }
    public DbSet<Subscription>? Subscriptions { get; set; }

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}
