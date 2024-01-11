using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IDataResult<int>> CountAsync()
    {
        int commentsCount = await _unitOfWork.Comments.CountAsync();
        if (commentsCount > -1)
            return new DataResult<int>(ResultStatus.Success, commentsCount);
        else
            return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen Bir Hata Oluştu!", -1);
    }

    public async Task<IDataResult<int>> CountByNonDeletedAsync()
    {
        int commentsCount = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted);
        if (commentsCount > -1)
            return new DataResult<int>(ResultStatus.Success, commentsCount);
        else
            return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen Bir Hata Oluştu!", -1);
    }
}