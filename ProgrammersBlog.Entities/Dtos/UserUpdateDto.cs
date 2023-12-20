using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos;

public class UserUpdateDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    [DataType(DataType.Upload)]
    public IFormFile PictureFile { get; set; }
    public string Picture { get; set; }
}