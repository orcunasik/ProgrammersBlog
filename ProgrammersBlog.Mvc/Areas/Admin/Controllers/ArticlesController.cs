using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Mvc.Helpers.Abstracts;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
public class ArticlesController : BaseController
{
    private readonly IArticleService _articleService;
    private readonly ICategoryService _categoryService;
    private readonly IToastNotification _toastNotification;

    public ArticlesController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper, IToastNotification toastNotification) : base(userManager, mapper, imageHelper)
    {
        _articleService = articleService;
        _categoryService = categoryService;
        _toastNotification = toastNotification;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        IDataResult<ArticleListDto> result = await _articleService.GetAllByNonDeletedAsync();
        if (result.ResultStatus == ResultStatus.Success)
            return View(result.Data);
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        IDataResult<CategoryListDto> result = await _categoryService.GetAllByNonDeletedAndActiveAsync();
        if(result.ResultStatus == ResultStatus.Success)
        {
            return View(new ArticleAddViewModel
            {
                Categories = result.Data.Categories
        });
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
    {
        if (ModelState.IsValid)
        {
            ArticleAddDto articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
            IDataResult<ImageUploadedDto> imageResult = await ImageHelper.UploadAsync(articleAddViewModel.Title, articleAddViewModel.ThumbnailFile, PictureType.Post);
            articleAddDto.Thumbnail = imageResult.Data.FullName;
            var result = await _articleService.AddAsync(articleAddDto, LoggedInUser.UserName, LoggedInUser.Id);
            if(result.ResultStatus == ResultStatus.Success)
            {
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }
        }
        IDataResult<CategoryListDto> categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
        articleAddViewModel.Categories = categories.Data.Categories;
        return View(articleAddViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int articleId)
    {
        IDataResult<ArticleUpdateDto> articleResult = await _articleService.GetArticleUpdateDtoAsync(articleId);
        IDataResult<CategoryListDto> categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
        if(articleResult.ResultStatus == ResultStatus.Success && categoriesResult.ResultStatus == ResultStatus.Success)
        {
            ArticleUpdateViewModel articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
            articleUpdateViewModel.Categories = categoriesResult.Data.Categories;
            return View(articleUpdateViewModel);
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
    {
        if (ModelState.IsValid)
        {
            bool isNewThumbnailUploaded = false;
            string oldThumbnail = articleUpdateViewModel.Thumbnail;
            if(articleUpdateViewModel.ThumbnailFile is not null)
            {
                IDataResult<ImageUploadedDto> uploadedImageResult = await ImageHelper.UploadAsync(articleUpdateViewModel.Title, articleUpdateViewModel.ThumbnailFile, PictureType.Post);
                articleUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Success
                    ? uploadedImageResult.Data.FullName
                    : "postImages/defaultThumbnail.jpg";
                if(oldThumbnail != "postImages/defaultThumbnail.jpg")
                {
                    isNewThumbnailUploaded = true;
                }
            }
            ArticleUpdateDto articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
            var result = await _articleService.UpdateAsync(articleUpdateDto, LoggedInUser.UserName);
            if (result.ResultStatus == ResultStatus.Success)
            {
                if (isNewThumbnailUploaded)
                {
                    ImageHelper.Delete(oldThumbnail);
                }
                _toastNotification.AddSuccessToastMessage(result.Message, new ToastrOptions
                {
                    Title = "Başarılı İşlem!"
                });
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError("", result.Message);
            }
        }
        IDataResult<CategoryListDto> categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
        articleUpdateViewModel.Categories = categories.Data.Categories;
        return View(articleUpdateViewModel);
    }

    [HttpPost]
    public async Task<JsonResult> Delete(int articleId)
    {
        var result = await _articleService.DeleteAsync(articleId, LoggedInUser.UserName);
        string articleResult = JsonSerializer.Serialize(result);
        return Json(articleResult);
    }

    [HttpGet]
    public async Task<JsonResult> GetAllArticles()
    {
        var articles = await _articleService.GetAllByNonDeletedAndActiveAsync();
        string articleResult = JsonSerializer.Serialize(articles, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        });
        return Json(articleResult);
    }
}
