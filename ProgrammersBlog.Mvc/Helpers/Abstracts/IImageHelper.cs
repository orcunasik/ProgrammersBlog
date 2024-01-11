using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Mvc.Helpers.Abstracts;

public interface IImageHelper
{
    Task<IDataResult<ImageUploadedDto>> UploadUserImageAsync(string userName, IFormFile pictureFile, string folderName ="userImages");
    IDataResult<ImageDeletedDto> Delete(string pictureName);
}