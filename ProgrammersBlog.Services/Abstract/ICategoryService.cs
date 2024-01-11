﻿using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Services.Abstract;

public interface ICategoryService
{
    Task<IDataResult<CategoryDto>> GetAsync(int categoryId);

    /// <summary>
    /// Verilen Id parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
    /// </summary>
    /// <param name="categoryId">0'dan büyük integer bir Id değeri</param>
    /// <returns>Asenkron bir operasyon ile Task olarak işlem sonucunu DataResult tipinde geriye döner.</returns>
    Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId);
    Task<IDataResult<CategoryListDto>> GetAllAsync();
    Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();
    Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync();

    /// <summary>
    /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler.
    /// </summary>
    /// <param name="categoryAddDto">categoryAddDto tipinde eklenecek kategori bilgileri</param>
    /// <param name="createdByName">string tipinde kullanıcının kullanıcı adı</param>
    /// <returns>Asenkron bir operasyon ile Task olarak bizlere ekleme işlemin sonucunu DataResult tipinde döner</returns>
    Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName);
    Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
    Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName);
    Task<IDataResult<int>> CountAsync();
    Task<IDataResult<int>> CountByNonDeletedAsync();
    Task<IResult> HardDeleteAsync(int categoryId);
}