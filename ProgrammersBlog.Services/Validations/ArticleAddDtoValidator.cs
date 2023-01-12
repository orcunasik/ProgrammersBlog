using FluentValidation;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Validations
{
    public class ArticleAddDtoValidator :AbstractValidator<ArticleAddDto>
    {
        public ArticleAddDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotNull().WithName("Başlık").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("Başlık").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır")
                .MaximumLength(100).WithName("Başlık").WithMessage("${PropertyName} Alanı 100 Karakterden Fazla Olmamalıdır")

                .MinimumLength(5).WithName("Başlık").WithMessage("${PropertyName} Alanı 5 Karakterden Az Olmamalıdır");
            RuleFor(x => x.Content)
                .NotNull().WithName("İçerik").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("İçerik").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır")
                .MinimumLength(20).WithName("İçerik").WithMessage("${PropertyName} Alanı 20 Karakterden Az Olmamalıdır");

            RuleFor(x => x.Thumbnail)
                .NotNull().WithName("Thumbnail").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("Thumbnail").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır")
                .MaximumLength(250).WithName("Thumbnail").WithMessage("${PropertyName} Alanı 250 Karakterden Fazla Olmamalıdır")
                .MinimumLength(5).WithName("Thumbnail").WithMessage("${PropertyName} Alanı 5 Karakterden Az Olmamalıdır");

            RuleFor(x => x.Date)
                .NotNull().WithName("Tarih").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("Tarih").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır");

            RuleFor(x => x.SeoAuthor)
                .NotNull().WithName("SeoYazar").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("SeoYazar").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır")
                .MaximumLength(50).WithName("SeoYazar").WithMessage("${PropertyName} Alanı 50 Karakterden Fazla Olmamalıdır")
                .MinimumLength(3).WithName("SeoYazar").WithMessage("${PropertyName} Alanı 3 Karakterden Az Olmamalıdır");

            RuleFor(x => x.SeoDescription)
                .NotNull().WithName("SeoAçıklama").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("SeoAçıklama").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır")
                .MaximumLength(150).WithName("SeoAçıklama").WithMessage("${PropertyName} Alanı 150 Karakterden Fazla Olmamalıdır")
                .MinimumLength(1).WithName("SeoAçıklama").WithMessage("${PropertyName} Alanı 1 Karakterden Az Olmamalıdır");

            RuleFor(x => x.SeoTags)
                .NotNull().WithName("SeoEtiketler").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("SeoEtiketler").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır")
                .MaximumLength(70).WithName("SeoEtiketler").WithMessage("${PropertyName} Alanı 70 Karakterden Fazla Olmamalıdır")
                .MinimumLength(3).WithName("SeoEtiketler").WithMessage("${PropertyName} Alanı 3 Karakterden Az Olmamalıdır");

            RuleFor(x => x.CategoryId)
                .NotNull().WithName("Kategori").WithMessage("${PropertyName} Alanı Gereklidir")
                .NotEmpty().WithName("Kategori").WithMessage("${PropertyName} Alanı Boş Bırakılmamalıdır");

            RuleFor(x => x.IsActive).NotNull().WithName("Aktif Mi?").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(x => x.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName} Boş Geçilmemelidir!");

            RuleFor(x => x.IsDeleted).NotNull().WithName("Silindi Mi?").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(x => x.IsDeleted).NotEmpty().WithName("Silindi Mi?").WithMessage("{PropertyName} Boş Geçilmemelidir!");
        }
    }
}
