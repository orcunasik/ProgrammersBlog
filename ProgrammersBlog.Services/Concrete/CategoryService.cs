﻿using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryAddDto.Name} adlı kategori başarı ile eklenmiştir", new CategoryDto
            {
                Category = addedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryAddDto.Name} adlı kategori başarı ile eklenmiştir"
            });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            Category oldCategory = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            Category category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCategory);
            category.ModifiedByName = modifiedByName;
            Category updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Success, $"{categoryUpdateDto.Name} adlı kategori başarı ile güncellenmiştir", new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Success,
                Message = $"{categoryUpdateDto.Name} adlı kategori başarı ile güncellenmiştir"
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Success, $"{deletedCategory.Name} adlı kategori başarı ile silinmiştir.", new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Success,
                    Message = $"{deletedCategory.Name} adlı kategori başarı ile silinmiştir."
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle Bir Kategori Bulunamadı!", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle Bir Kategori Bulunamadı"
            });
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, "${categoryUpdateDto.Name} adlı kategori başarı ile veritabanından silinmiştir");
            }
            return new Result(ResultStatus.Error, "Böyle Bir Kategori Bulunamadı");
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
           var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c => c.Articles);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStatus.Success,new CategoryDto
                {
                    Category = category,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle Bir Kategori Bulunamadı", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle Bir Kategori Bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir Kategori Bulunamadı", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiçbir Kategori Bulunamadı!"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted,c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir Kategori Bulunamadı", new CategoryListDto
            {
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiçbir Kategori Bulunamadı!"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            IList<Category> categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir Kategori Bulunamadı", null);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            bool result = await _unitOfWork.Categories.AnyAsync(c => c.Id == categoryId);
            if (result)
            {
                Category category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                CategoryUpdateDto categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Success, categoryUpdateDto);
            }
            else
            {
                return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı!",null);
            }
        }
    }
}
