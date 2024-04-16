using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model;

namespace DataModel.Model
{
    public class ProjectDataModel
    {
        public long Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public ProjectDataModel() {}

        public ProjectDataModel(Project project)
        {
            Id = project.Id;
            StartDate = project.StartDate;
            EndDate = project.EndDate;
        }
    }
}