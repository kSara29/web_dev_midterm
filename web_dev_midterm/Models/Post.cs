using web_dev_midterm.Models.Common;

namespace web_dev_midterm.Models;

public sealed class Post: Entity
{
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Like> Likes { get; set; } = new();
    public List<PostComment> Comments { get; set; } = new();
    
    public string UserId { get; set;  }
    public User? User { get; set; }
    
    public Post(
        string description,
        string userId
    )
    {
        Description = description;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
    
    private Post(){}
}
