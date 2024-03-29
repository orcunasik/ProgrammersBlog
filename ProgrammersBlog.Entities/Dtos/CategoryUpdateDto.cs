﻿namespace ProgrammersBlog.Entities.Dtos;

public class CategoryUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public string Note { get; set; }
}