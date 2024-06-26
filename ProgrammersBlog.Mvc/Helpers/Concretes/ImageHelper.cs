﻿using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Mvc.Helpers.Abstracts;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Mvc.Helpers.Concretes;

public class ImageHelper : IImageHelper
{
    private readonly IWebHostEnvironment _env;
    private readonly string _wwwroot;
    private const string imgFolder = "img";
    private const string userImagesFolder = "userImages";
    private const string postImagesFolder = "postImages";

    public ImageHelper(IWebHostEnvironment env)
    {
        _env = env;
        _wwwroot = _env.WebRootPath;
    }

    public IDataResult<ImageDeletedDto> Delete(string pictureName)
    {
        
        string fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
        if (File.Exists(fileToDelete))
        {
            FileInfo fileInfo = new FileInfo(fileToDelete);
            ImageDeletedDto imageDeletedDto = new ImageDeletedDto
            {
                FullName = pictureName,
                Extension = fileInfo.Extension,
                Path = fileInfo.FullName,
                Size = fileInfo.Length
            };
            File.Delete(fileToDelete);
            return new DataResult<ImageDeletedDto>(ResultStatus.Success, imageDeletedDto);
        }
        else
            return new DataResult<ImageDeletedDto>(ResultStatus.Error, "Böyle Bir Resim Bulunamadı!",null);
    }

    public async Task<IDataResult<ImageUploadedDto>> UploadAsync(string name, IFormFile pictureFile, PictureType pictureType ,string folderName = null)
    {
        folderName ??= pictureType == PictureType.User ? userImagesFolder : postImagesFolder;

        if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
        {
            Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
        }

        string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
        string fileExtension = Path.GetExtension(pictureFile.FileName);
        DateTime dateTime = DateTime.Now;
        string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
        string path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
        await using (FileStream stream = new FileStream(path, FileMode.Create))
        {
            await pictureFile.CopyToAsync(stream);
        }

        string message = pictureType == PictureType.User
            ? $"{name} adlı kullanıcının resmi başarıyla yüklenmiştir"
            : $"{name} adlı makalenin resmi başarıyla yüklenmiştir";

        return new DataResult<ImageUploadedDto>(ResultStatus.Success, message, new ImageUploadedDto
        {
            FullName = $"{folderName}/{newFileName}",
            OldName = oldFileName,
            Extension = fileExtension,
            Path = path,
            FolderName = folderName,
            Size = pictureFile.Length
        });
    }
}
