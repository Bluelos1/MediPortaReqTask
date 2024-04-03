using MediportaTask.Model;

namespace MediportaTask.Interface;

public interface IStackOverflowService
{
    Task<IEnumerable<Tag>> GetTagsAsync(int page, int pageSize, string sortOrder, string sortField);
}
