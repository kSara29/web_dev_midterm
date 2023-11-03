using web_dev_midterm.Models;

namespace web_dev_midterm.ViewModels.Home;

public class HomeVm
{
    public bool Liked = false;
    public string CurrentUserId { get; set; }
    public string UserComment { get; set; }
    public List<Post> Posts { get; set; } = new();

}