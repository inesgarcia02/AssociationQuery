using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Model;
using Domain.Model;

namespace DataModel.Mapper
{
    public class ProjectMapper
    {

        public ProjectMapper()
        {
        }

        public Project ToDomain(ProjectDataModel projectDM)
        {
            return new Project(projectDM.Id, projectDM.StartDate, projectDM.EndDate);
        }

        public IEnumerable<Project> ToDomain(IEnumerable<ProjectDataModel> projectsDataModel)
        {
            List<Project> projectsDomain = new List<Project>();

            foreach(ProjectDataModel projectDomain in projectsDataModel)
            {
                Project project = ToDomain(projectDomain);

                projectsDomain.Add(project);
            }

            return projectsDomain.AsEnumerable();
        }

        public ProjectDataModel ToDataModel(Project project)
        {
            ProjectDataModel projectDataModel = new ProjectDataModel(project);

            return projectDataModel;
        }
    }
}