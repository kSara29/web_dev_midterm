﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_dev_midterm.Models;
using web_dev_midterm.Persistence;
using web_dev_midterm.ViewModels.Home;
using web_dev_midterm.ViewModels.Post;

namespace web_dev_midterm.Controllers;

public class PostController: Controller
{
    private readonly AppDbContext _db;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public PostController(AppDbContext db, 
        UserManager<User> userManager, 
        SignInManager<User> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Add(PostAddVm model)
    {
        if (!ModelState.IsValid)
            return View();
        
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound();

        Post? post = new Post(
            model.Description,
            user.Id
        );
        
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();

        return RedirectToAction("Profile", "User", new { userId = user.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Details(long postId, string userId)
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var usertarget = await _userManager.FindByIdAsync(userId);
        
        var userSource = String.Empty;
        var userTarget = String.Empty;

        if (currentUser.Id == userId)
        {
            userSource = userId;
            userTarget = userId;
        }
        else
        {
            userSource = currentUser.Id;
            userTarget = userId;
        }
        
        var posts = _db.Posts.Where(p => p.Id == postId)
            .Include(p => p.User)
            .Include(p => p.Likes)
            .Include(p => p.Comments.OrderByDescending(c => c.CreatedAt))
            .ToList();
        
        foreach (var post in posts)
        {
            foreach (var comment in post.Comments)
            {
                _db.Entry(comment)
                    .Reference(c => c.User)
                    .Load();
            }
        }
        
        var vm = new HomeVm
        {
            CurrentUserId = currentUser.Id,
            Posts = posts,
            SourceId = userSource,
            TargetId = userTarget
            
        };

        return View(vm);
    }

    
    [HttpPost]
    public async Task<IActionResult> Details(HomeVm vm, long postId)
    {
        //var userProfile = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        var user = await _userManager.GetUserAsync(User);
        var like = new Like(user.Id, postId);
        
        if (vm.UserComment is not null)
        {
            var comment = new PostComment(postId, user.Id, vm.UserComment);
            await _db.PostComments.AddAsync(comment);
            await _db.SaveChangesAsync();
        }
        
        var likeCheck = _db.Likes
            .FirstOrDefault(s => s.UserId == user.Id && s.PostId == postId);

        bool check = false;

        if (likeCheck != null)
        {
            _db.Likes.Remove(likeCheck);
            await _db.SaveChangesAsync();
        }
        else
        {
            await _db.Likes.AddAsync(like);
            await _db.SaveChangesAsync();
        }
        
        var posts = _db.Posts.Where(p => p.Id == postId)
            .Include(p => p.User)
            .Include(p => p.Comments.OrderByDescending(c => c.CreatedAt))
            .Include(p => p.Likes)
            .ToList();
        
        foreach (var post in posts)
        {
            foreach (var comment in post.Comments)
            {
                _db.Entry(comment)
                    .Reference(c => c.User)
                    .Load();
            }
        }
        
        vm = new HomeVm
        {
            CurrentUserId = user.Id,
            Posts = posts
        };
        
        return View(vm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(long postId)
    {
        if (!ModelState.IsValid)
            return NotFound();
        
        var user = await _userManager.GetUserAsync(User);
        
        if (user == null)
            return NotFound();

        var post = _db.Posts.FirstOrDefault(p => p.Id == postId);

        _db.Posts.Remove(post);
        _db.SaveChanges();
        
        return RedirectToAction("Profile", "User", new {userId = user.Id});
    }

    [HttpGet]
    public IActionResult Edit(long postId, string userId)
    {
        PostEditVm vm = new PostEditVm
        {
            Id = postId,
            Description = "",
            targetUser = userId
        };
        
        return View(vm);
    }
    
    [HttpPost]
    public IActionResult EditPost(long postId, string userId)
    {
        PostEditVm vm = new PostEditVm
        {
            Id = postId,
            Description = "",
            targetUser = userId
        };
        
        return RedirectToAction("Profile", "User", new {userId = userId});;
    }
    
    [HttpGet]
    public async Task<IActionResult> SendComment(string comment, long postId)
    {
        var post = _db.Posts.FirstOrDefault(p => p.Id == postId);
        var user = await _userManager.GetUserAsync(User);

        var userId = user.Id;
        if (post == null)
            return NotFound();

        post.Description = comment;
        
        _db.Posts.Update(post);
        _db.SaveChanges();
        return RedirectToAction("Profile", "User", new { userId = userId });
    }

}
