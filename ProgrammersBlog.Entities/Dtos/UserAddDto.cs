using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserAddDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}
