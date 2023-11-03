using System.ComponentModel.DataAnnotations;

namespace web_dev_midterm.ViewModels.Post;

public class PostAddVm
{
    [Required(ErrorMessage = "Поле обязательно")]
    public string Description { get; set; }

}