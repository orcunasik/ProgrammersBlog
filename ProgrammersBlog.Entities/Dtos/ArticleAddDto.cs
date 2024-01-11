using ProgrammersBlog.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos;

public class ArticleAddDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Thumbnail { get; set; }

    [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:dd/mm/yyyy}")]
    public DateTime Date { get; set; }
    public string SeoAuthor { get; set; }
    public string SeoDescription { get; set; }
    public string SeoTags { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

}