using FluentValidation;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Services.Validations;

public class UserPasswordChangeDtoValidator : AbstractValidator<UserPasswordChangeDto>
{
    public UserPasswordChangeDtoValidator()
    {
        RuleFor(u => u.CurrentPassword).NotNull().WithName("Mevcut Şifre").WithMessage("{PropertyName} Alanı Gereklidir!");
        RuleFor(u => u.CurrentPassword).NotEmpty().WithName("Mevcut Şifre").WithMessage("{PropertyName} Boş Geçilmemelidir!");
        RuleFor(u => u.CurrentPassword).MaximumLength(30).WithName("Mevcut Şifre").WithMessage("{PropertyName} 30 Karakterden Fazla Olmamalıdır!");
        RuleFor(u => u.CurrentPassword).MinimumLength(5).WithName("Mevcut Şifre").WithMessage("{PropertyName} 5 Karakterden Az Olmamalıdır!");

        RuleFor(u => u.NewPassword).NotNull().WithName("Yeni Şifre").WithMessage("{PropertyName} Alanı Gereklidir!");
        RuleFor(u => u.NewPassword).NotEmpty().WithName("Yeni Şifre").WithMessage("{PropertyName} Boş Geçilmemelidir!");
        RuleFor(u => u.NewPassword).MaximumLength(30).WithName("Yeni Şifre").WithMessage("{PropertyName} 30 Karakterden Fazla Olmamalıdır!");
        RuleFor(u => u.NewPassword).MinimumLength(5).WithName("Yeni Şifre").WithMessage("{PropertyName} 5 Karakterden Az Olmamalıdır!");

        RuleFor(u => u.RepeatPassword).NotNull().WithName("Yeni Şifre Tekrarı").WithMessage("{PropertyName} Alanı Gereklidir!");
        RuleFor(u => u.RepeatPassword).NotEmpty().WithName("Yeni Şifre Tekrarı").WithMessage("{PropertyName} Boş Geçilmemelidir!");
        RuleFor(u => u.RepeatPassword).MaximumLength(30).WithName("Yeni Şifre Tekrarı").WithMessage("{PropertyName} 30 Karakterden Fazla Olmamalıdır!");
        RuleFor(u => u.RepeatPassword).MinimumLength(5).WithName("Yeni Şifre Tekrarı").WithMessage("{PropertyName} 5 Karakterden Az Olmamalıdır!");
        RuleFor(u => u.RepeatPassword).Must((dto, repeatPassword) => repeatPassword == dto.NewPassword)
                    .WithMessage("Yeni Şifre ve Yeni Şifre Tekrarı alanları uyumlu değil!");
    }
}