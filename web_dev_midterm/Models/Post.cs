﻿using web_dev_midterm.Models.Common;

namespace web_dev_midterm.Models;

public sealed class Post: Entity
{
    public byte[] Image { get; }
    public string Description { get; }
    public DateTime CreatedAt { get; set; }
    public List<Like> Likes { get; set; } = new();
    public List<PostComment> Comments { get; set; } = new();
    
    public string UserId { get; set;  }
    public User? User { get; set; }
    
    public Post(
        byte[] image,
        string description,
        string userId
    )
    {
        Image = image;
        Description = description;
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
    }
    
    private Post(){}
}
