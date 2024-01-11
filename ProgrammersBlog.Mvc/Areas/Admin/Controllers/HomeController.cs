using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Editor")]
public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IArticleService _articleService;
    private readonly ICommentService _commentService;
    private readonly UserManager<User> _userManager;

    public HomeController(ICategoryService categoryService, IArticleService articleService, ICommentService commentService, UserManager<User> userManager)
    {
        _categoryService = categoryService;
        _articleService = articleService;
        _commentService = commentService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        IDataResult<int> categoriesCountResult = await _categoryService.CountByNonDeletedAsync();
        IDataResult<int> articlesCountResult = await _articleService.CountByNonDeletedAsync();
        IDataResult<int> commentsCountResult = await _commentService.CountByNonDeletedAsync();
        int usersCount = await _userManager.Users.CountAsync();
        IDataResult<ArticleListDto> articlesResult = await _articleService.GetAllAsync();

        if(categoriesCountResult.ResultStatus == Shared.Utilities.Results.ComplexTypes.ResultStatus.Success && articlesCountResult.ResultStatus == Shared.Utilities.Results.ComplexTypes.ResultStatus.Success && commentsCountResult.ResultStatus == Shared.Utilities.Results.ComplexTypes.ResultStatus.Success && 
            usersCount > -1 && 
            articlesResult.ResultStatus == Shared.Utilities.Results.ComplexTypes.ResultStatus.Success)
        {
            return View(new DashboardViewModel
            {
                CategoriesCount = categoriesCountResult.Data,
                ArticlesCount = articlesCountResult.Data,
                CommentsCount = commentsCountResult.Data,
                UsersCount = usersCount,
                Articles = articlesResult.Data
            });
        }
        return NotFound();

    }
}