using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos;

public class ArticleListDto: DtoGetBase
{
    public IList<Article> Articles { get; set; }
    
}