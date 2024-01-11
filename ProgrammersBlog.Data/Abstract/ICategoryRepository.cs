﻿using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Abstract;

namespace ProgrammersBlog.Data.Abstract;

public interface ICategoryRepository : IEntityRepository<Category>
{
    Task<Category> GetByIdAsync(int categoryId);
}