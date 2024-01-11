using FluentValidation;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Services.Validations;

public class CategoryAddDtoValidator : AbstractValidator<CategoryAddDto>
{
    public CategoryAddDtoValidator() 
    {
        RuleFor(c => c.Name).NotNull().WithName("Kategori Adı").WithMessage("{PropertyName} Alanı Gereklidir!");
        RuleFor(c => c.Name).NotEmpty().WithName("Kategori Adı").WithMessage("{PropertyName} Boş Geçilmemelidir!");
        RuleFor(c => c.Name).MaximumLength(100).WithName("Kategori Adı").WithMessage("{PropertyName} Alanı 100 Karakterden Fazla Olmamalıdır!");
        RuleFor(c => c.Name).MinimumLength(2).WithName("Kategori Adı").WithMessage("{PropertyName} Alanı 2 Karakterden Az Olmamalıdır!");

        RuleFor(c => c.Description).MaximumLength(500).WithName("Kategori Açıklaması").WithMessage("{PropertyName}Alanı 500 Karakterden Fazla Olmamalıdır!");
        RuleFor(c => c.Description).MinimumLength(3).WithName("Kategori Açıklaması").WithMessage("{PropertyName}Alanı 3 Karakterden Az Olmamalıdır!");

        RuleFor(c => c.Note).MaximumLength(500).WithName("Kategori Özel Not").WithMessage("{PropertyName}Alanı 500 Karakterden Fazla Olmamalıdır!");
        RuleFor(c => c.Note).MinimumLength(3).WithName("Kategori Özel Not").WithMessage("{PropertyName} Alanı 3 Karakterden Az Olmamalıdır!");

        RuleFor(c => c.IsActive).NotNull().WithName("Aktif Mi?").WithMessage("{PropertyName} Alanı Gereklidir!");
        RuleFor(c => c.IsActive).NotEmpty().WithName("Aktif Mi?").WithMessage("{PropertyName} Boş Geçilmemelidir!");
    }
}