using Domain.Model;

namespace Domain.IRepository;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetProjectsIdAsync();
    Task<Project> Add(Project project);
    Task<bool> ProjectExists(long projectId);
    Task<Project> GetProjectsByIdAsync(long id);
}