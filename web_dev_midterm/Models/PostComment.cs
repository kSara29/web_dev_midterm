using web_dev_midterm.Models.Common;

namespace web_dev_midterm.Models;

public class PostComment: Entity
{
    public long PostId { get; }
    public Post? Post { get; set; }
    
    public string UserId { get; }
    public User? User { get; set; }
    
    public string Comment { get; }
    public DateTime CreatedAt { get; set; }

    public PostComment(
        long postId,
        string userId,
        string comment)
    {
        PostId = postId;
        UserId = userId;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }
    
    private PostComment(){}
    
}
