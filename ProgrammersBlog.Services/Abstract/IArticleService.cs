﻿using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Services.Abstract;
public interface IArticleService
{
    Task<IDataResult<ArticleDto>> GetAsync(int articleId);
    Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsync(int articleId);
    Task<IDataResult<ArticleListDto>> GetAllAsync();
    Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync();
    Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync();
    Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId);
    Task<IDataResult<int>> CountAsync();
    Task<IDataResult<int>> CountByNonDeletedAsync();
    Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName, int userId);
    Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName);
    Task<IResult> DeleteAsync(int articleId, string modifiedByName);
    Task<IResult> HardDeleteAsync(int articleId);
}