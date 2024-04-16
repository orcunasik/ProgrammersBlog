using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilies;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using static ProgrammersBlog.Services.Utilies.Messages;
using Article = ProgrammersBlog.Entities.Concrete.Article;

namespace ProgrammersBlog.Services.Concrete;
public class ArticleService : IArticleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName, int userId)
    {
        Article article = _mapper.Map<Article>(articleAddDto);
        article.CreatedByName = createdByName;
        article.ModifiedByName = createdByName;
        article.UserId = userId;
        await _unitOfWork.Articles.AddAsync(article);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, Messages.Article.Add(article.Title));
    }

    public async Task<IDataResult<int>> CountAsync()
    {
        int articlesCount = await _unitOfWork.Articles.CountAsync();
        if (articlesCount > -1)
            return new DataResult<int>(ResultStatus.Success, articlesCount);
        else
            return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen Bir Hata Oluştu!", -1);
    }

    public async Task<IDataResult<int>> CountByNonDeletedAsync()
    {
        int articlesCount = await _unitOfWork.Articles.CountAsync(a => !a.IsDeleted);
        if (articlesCount > -1)
            return new DataResult<int>(ResultStatus.Success, articlesCount);
        else
            return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen Bir Hata Oluştu!", -1);
    }

    public async Task<IResult> DeleteAsync(int articleId, string modifiedByName)
    {
        bool result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
        if (result) 
        {
            Article article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
            article.IsDeleted = true;
            article.ModifiedByName = modifiedByName;
            article.ModifiedDate = DateTime.Now;
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.Delete(article.Title));
        }
        return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural:false));
    }

    public async Task<IDataResult<ArticleDto>> GetAsync(int articleId)
    {
        Article article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId,a => a.User, a => a.Category);
        if (article != null)
        {
            return new DataResult<ArticleDto>(ResultStatus.Success, new ArticleDto
            {
                Article = article,
                ResultStatus = ResultStatus.Success
            });
        }
        return new DataResult<ArticleDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:false), null);
    }

    public async Task<IDataResult<ArticleListDto>> GetAllAsync()
    {
        IList<Article> articles = await _unitOfWork.Articles.GetAllAsync(null, a => a.User, a => a.Category);
        if (articles.Count > -1)
        {
            return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
            {
                Articles = articles,
                ResultStatus = ResultStatus.Success
            });
        }
        return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:true), null);
    }

    public async Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId)
    {
        bool result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
        if (result)
        {
            IList<Article> articles = await _unitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:true), null);
        }
        return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural:false), null);
    }

    public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync()
    {
        IList<Article> articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted, a=> a.User, a => a.Category);
        if (articles.Count > -1)
        {
            return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
            {
                Articles = articles,
                ResultStatus = ResultStatus.Success
            });
        }
        return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:true), null);
    }

    public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync()
    {
        IList<Article> articles = await _unitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
        if (articles.Count > -1)
        {
            return new DataResult<ArticleListDto>(ResultStatus.Success, new ArticleListDto
            {
                Articles = articles,
                ResultStatus = ResultStatus.Success
            });
        }
        return new DataResult<ArticleListDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:true), null);
    }

    public async Task<IResult> HardDeleteAsync(int articleId)
    {
        bool result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
        if (result)
        {
            Article article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
            await _unitOfWork.Articles.DeleteAsync(article);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, Messages.Article.HardDelete(article.Title));
        }
        return new Result(ResultStatus.Error, Messages.Article.NotFound(isPlural:false));
    }

    public async Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName)
    {
        Article oldArticle = await _unitOfWork.Articles.GetAsync(a => a.Id == articleUpdateDto.Id);
        Article article = _mapper.Map(articleUpdateDto, oldArticle);
        article.ModifiedByName = modifiedByName;
        await _unitOfWork.Articles.UpdateAsync(article);
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, Messages.Article.Update(article.Title));
    }

    public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsync(int articleId)
    {
        bool result = await _unitOfWork.Articles.AnyAsync(a => a.Id == articleId);
        if (result)
        {
            Article article = await _unitOfWork.Articles.GetAsync(a => a.Id == articleId);
            ArticleUpdateDto articleUpdateDto = _mapper.Map<ArticleUpdateDto>(article);
            return new DataResult<ArticleUpdateDto>(ResultStatus.Success, articleUpdateDto);
        }
        else
        {
            return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
        }
    }
}