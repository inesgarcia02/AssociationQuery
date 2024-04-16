
using Domain.Model;

namespace Application.DTO
{
    public class ProjectDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public ProjectDTO()
        {
        }

        public ProjectDTO(long id, string name, DateOnly startDate, DateOnly? endDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }


        static public Project ToDomain(ProjectDTO projectDTO)
        {
            Project project = new Project(projectDTO.Id, projectDTO.StartDate, projectDTO.EndDate);

            return project;
        }
    }
}