using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Mvc.Helpers.Abstracts;

public interface IImageHelper
{
    Task<IDataResult<ImageUploadedDto>> UploadAsync(string name, IFormFile pictureFile, PictureType pictureType, string folderName = null);
    IDataResult<ImageDeletedDto> Delete(string pictureName);
}