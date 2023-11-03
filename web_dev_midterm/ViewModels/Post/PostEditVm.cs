using System.ComponentModel.DataAnnotations;

namespace web_dev_midterm.ViewModels.Post;

public class PostEditVm
{
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Поле обязательно")]
    public string Description { get; set; }
    
    public string targetUser { get; set; }

}