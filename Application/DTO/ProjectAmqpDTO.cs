using System;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Application.DTO
{
    public class ProjectAmqpDTO
    {
        public string Identifier { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }

        public ProjectAmqpDTO()
        {
        }

        public ProjectAmqpDTO( long id, string name, DateOnly startDate, DateOnly? endDate)
        {
            Identifier = "Project";
            Id = id;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public static string Serialize(ProjectDTO projectDTO)
        {
            ProjectAmqpDTO projectGateway = new ProjectAmqpDTO(projectDTO.Id, projectDTO.Name, projectDTO.StartDate, projectDTO.EndDate);
            var jsonMessage = JsonSerializer.Serialize(projectGateway);
            return jsonMessage;
        }

        public static ProjectAmqpDTO Deserialize(string projectDTOString)
        {
            return JsonSerializer.Deserialize<ProjectAmqpDTO>(projectDTOString);
        }

        public static ProjectDTO ToDTO(string projectDTOString)
        {
            ProjectAmqpDTO projectGatewayDTO = Deserialize(projectDTOString);
            ProjectDTO projectDTO = new ProjectDTO(projectGatewayDTO.Id, projectGatewayDTO.Name, projectGatewayDTO.StartDate,
                projectGatewayDTO.EndDate);
            return projectDTO;
        }
    }
}