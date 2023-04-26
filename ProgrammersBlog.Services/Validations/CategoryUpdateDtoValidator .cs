using FluentValidation;
using ProgrammersBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Validations
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator() 
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();

            RuleFor(c => c.Name).NotNull().WithName("Kategori Adı").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(c => c.Name).NotEmpty().WithName("Kategori Adı").WithMessage("{PropertyName} Boş Geçilmemelidir!");
            RuleFor(c => c.Name).MaximumLength(100).WithName("Kategori Adı").WithMessage("{PropertyName}Alanı 100 Karakterden Fazla Olmamalıdır!");
            RuleFor(c => c.Name).MinimumLength(3).WithName("Kategori Adı").WithMessage("{PropertyName} Alanı 3 Karakterden Az Olmamalıdır!");

            RuleFor(c => c.Description).MaximumLength(500).WithName("Kategori Açıklaması").WithMessage("{PropertyName}  Alanı 500 Karakterden Fazla Olmamalıdır!");
            RuleFor(c => c.Description).MinimumLength(3).WithName("Kategori Açıklaması").WithMessage("{PropertyName}    Alanı 3 Karakterden Az Olmamalıdır!");

            RuleFor(c => c.Note).MaximumLength(500).WithName("Kategori Özel Not").WithMessage("{PropertyName} Alanı 500 Karakterden Fazla Olmamalıdır!");
            RuleFor(c => c.Note).MinimumLength(3).WithName("Kategori Özel Not").WithMessage("{PropertyName} Alanı 3 Karakterden Az Olmamalıdır!");

            RuleFor(c => c.IsActive).NotNull().WithName("Aktif Mi?").WithMessage("{PropertyName} Alanı Gereklidir!");
            //RuleFor(c => c.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName} Boş Geçilmemelidir!");

            RuleFor(c => c.IsDeleted).NotNull().WithName("Silindi Mi?").WithMessage("{PropertyName} Alanı Gereklidir!");
            //RuleFor(c => c.IsDeleted).NotEmpty().WithName("Silindi Mi?").WithMessage("{PropertyName} Boş Geçilmemelidir!");
        }
    }
}
