namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Model;
using DataModel.Mapper;

using Domain.Model;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class ProjectRepository : GenericRepository<ProjectRepository>, IProjectRepository
{    
    ProjectMapper _projectMapper;
    public ProjectRepository(AbsanteeContext context, ProjectMapper mapper) : base(context!)
    {
        _projectMapper = mapper;
    }

    public async Task<IEnumerable<Project>> GetProjectsIdAsync()
    {
        try {
            IEnumerable<ProjectDataModel> projectsDataModel = await _context.Set<ProjectDataModel>()
                    .ToListAsync();

            IEnumerable<Project> projects = _projectMapper.ToDomain(projectsDataModel);

            return projects;
        }
        catch
        {
            throw;
        }
    }


    public async Task<Project> GetProjectsByIdAsync(long id)
    {
        try
        {
            ProjectDataModel projectDataModel = await _context.Set<ProjectDataModel>()
                .FirstAsync(p => p.Id == id);

            Project project = _projectMapper.ToDomain(projectDataModel);

            return project;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Project> Add(Project project)
    {
        try {
            ProjectDataModel projectsDataModel = _projectMapper.ToDataModel(project);

            EntityEntry<ProjectDataModel> projectDataModelEntityEntry = _context.Set<ProjectDataModel>().Add(projectsDataModel);
            
            await _context.SaveChangesAsync();

            ProjectDataModel projectDataModelSaved = projectDataModelEntityEntry.Entity;

            Project projectSaved = _projectMapper.ToDomain(projectDataModelSaved);

            return projectSaved;    
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> ProjectExists(long projectId)
    {
        return await _context.Set<ProjectDataModel>().AnyAsync(p => p.Id == projectId);
    }
}