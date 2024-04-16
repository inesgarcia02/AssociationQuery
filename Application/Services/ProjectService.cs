using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Domain.IRepository;
using Domain.Model;

namespace Application.Services
{
    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;
        
        public ProjectService(IProjectRepository projectRepository) {
            _projectRepository = projectRepository;
        }

        public async Task<Project> Add(ProjectDTO projectDTO)
        {
            Project project = ProjectDTO.ToDomain(projectDTO);
            return await _projectRepository.Add(project);
        }
    }
}