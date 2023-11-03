using Microsoft.AspNetCore.Mvc;
using web_dev_midterm.Persistence;

namespace web_dev_midterm.Controllers;

public class ValidationController: Controller
{
    private  AppDbContext _context;
    
    public ValidationController(AppDbContext context)
    {
        _context = context;
    }
    
    [AcceptVerbs("GET", "POST")]
    public bool CheckName(string userName)
    {
        var check = _context.Users.Any(u => u.UserName.ToLower() == userName.ToLower());
        return !check;
    }
    
    [AcceptVerbs("GET", "POST")]
    public bool EmailCheck(string email)
    {
        var check = _context.Users.Any(u => u.Email.ToLower() == email.ToLower());
        return !check;
    }
}
