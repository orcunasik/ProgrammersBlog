namespace ProgrammersBlog.Entities.Dtos;

public class UserPasswordChangeDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string RepeatPassword { get; set; }
}