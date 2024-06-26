﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Areas.Admin.Models;
using ProgrammersBlog.Mvc.Helpers.Abstracts;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgrammersBlog.Mvc.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Editor")]
public class CategoriesController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService,UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper) : base(userManager,mapper,imageHelper)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _categoryService.GetAllByNonDeletedAsync();
        return View(result.Data);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return PartialView("_CategoryAddPartial");
    }

    [HttpPost]
    public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.AddAsync(categoryAddDto, LoggedInUser.UserName);
            if(result.ResultStatus is ResultStatus.Success)
            {
                var categoryAddAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
                {
                    CategoryDto = result.Data,
                    CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
                });
                return Json(categoryAddAjaxModel);
            }
        }
        var categoryAddAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewModel
        {
            CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartial", categoryAddDto)
        });
        return Json(categoryAddAjaxErrorModel);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int categoryId)
    {
        var result = await _categoryService.GetCategoryUpdateDtoAsync(categoryId);
        if (result.ResultStatus == ResultStatus.Success)
            return PartialView("_CategoryUpdatePartial", result.Data);
        else
            return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoryService.UpdateAsync(categoryUpdateDto, LoggedInUser.UserName);
            if (result.ResultStatus is ResultStatus.Success)
            {
                string categoryUpdateAjaxModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
                {
                    CategoryDto = result.Data,
                    CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
                });
                return Json(categoryUpdateAjaxModel);
            }
        }
        string categoryUpdateAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
        {
            CategoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartial", categoryUpdateDto)
        });
        return Json(categoryUpdateAjaxErrorModel);
    }

    public async Task<JsonResult> GetAllCategories()
    {
        var result = await _categoryService.GetAllByNonDeletedAsync();
        string categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.Preserve
        });
        return Json(categories);
    }

    [HttpPost]
    public async Task<JsonResult> Delete(int categoryId)
    {
        var result = await _categoryService.DeleteAsync(categoryId, LoggedInUser.UserName);
        string deletedCategory = JsonSerializer.Serialize(result.Data);
        return Json(deletedCategory);
    }
}
