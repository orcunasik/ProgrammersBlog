using FluentValidation;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.Services.Validations
{
    public class UserAddDtoValidator : AbstractValidator<UserAddDto>
    {
        public UserAddDtoValidator()
        {
            RuleFor(u => u.UserName).NotNull().WithName("Kullanıcı Adı").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(u => u.UserName).NotEmpty().WithName("Kullanıcı Adı").WithMessage("{PropertyName} Boş Geçilmemelidir!");
            RuleFor(u => u.UserName).MaximumLength(50).WithName("Kullanıcı Adı").WithMessage("{PropertyName}Alanı 5 Karakterden Fazla Olmamalıdır!");
            RuleFor(u => u.UserName).MinimumLength(3).WithName("Kullanıcı Adı").WithMessage("{PropertyName}Alanı 3 Karakterden Az Olmamalıdır!");

            RuleFor(u => u.Email).NotNull().WithName("Eposta Adresi").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(u => u.Email).NotEmpty().WithName("Eposta Adresi").WithMessage("{PropertyName} Boş Geçilmemelidir!");
            RuleFor(u => u.Email).MaximumLength(100).WithName("Eposta Adresi").WithMessage("{PropertyName} Alanı 100 Karakterden Fazla Olmamalıdır!");
            RuleFor(u => u.Email).MinimumLength(10).WithName("Eposta Adresi").WithMessage("{PropertyName} Alanı 10 Karakterden Az Olmamalıdır!");
            RuleFor(u => u.Email)
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithName("Eposta Adresi")
                .WithMessage("Geçerli Bir {PropertyName} Değildir!");

            RuleFor(u => u.Password).NotNull().WithName("Parola").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(u => u.Password).NotEmpty().WithName("Parola").WithMessage("{PropertyName} Boş Geçilmemelidir!");
            RuleFor(u => u.Password).MaximumLength(30).WithName("Parola").WithMessage("{PropertyName} 30 Karakterden Fazla Olmamalıdır!");
            RuleFor(u => u.Password).MinimumLength(5).WithName("Parola").WithMessage("{PropertyName} 5 Karakterden Az Olmamalıdır!");

            RuleFor(u => u.PhoneNumber).NotNull().WithName("Telefon No").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithName("Telefon No").WithMessage("{PropertyName} Boş Geçilmemelidir!");
            RuleFor(u => u.PhoneNumber).MaximumLength(13).WithName("Telefon No").WithMessage("{PropertyName} 13 Karakterden Fazla Olmamalıdır!");
            RuleFor(u => u.PhoneNumber).MinimumLength(13).WithName("Telefon No").WithMessage("{PropertyName} 13 Karakterden Az Olmamalıdır!");

            RuleFor(u => u.PictureFile).NotNull().WithName("Profil Resmi").WithMessage("{PropertyName} Alanı Gereklidir!");
            RuleFor(u => u.PictureFile).NotEmpty().WithName("Profil Resmi").WithMessage("Lütfen Bir {PropertyName} Seçiniz!");


        }
    }
}
