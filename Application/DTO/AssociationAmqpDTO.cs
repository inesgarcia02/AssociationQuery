using System.Text.Json;
using Newtonsoft.Json;

namespace Application.DTO
{
    public class AssociationAmqpDTO
    {
        public long Id { get; set; }
        public long ColaboratorId { get; set; }
        public long ProjectId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }


        public AssociationAmqpDTO() { }

        public AssociationAmqpDTO(long id, long colabId, long projectId, DateOnly startDate, DateOnly endDate)
        {
            Id = id;
            ColaboratorId = colabId;
            ProjectId = projectId;
            StartDate = startDate;
            EndDate = endDate;
        }

        static public string Serialize(AssociationDTO associationDTO)
        {
            var jsonMessage = JsonConvert.SerializeObject(associationDTO);
            return jsonMessage;
        }

        static public AssociationDTO Deserialize(string jsonMessage)
        {
            var associationDTO = JsonConvert.DeserializeObject<AssociationDTO>(jsonMessage);
            return associationDTO!;
        }

    }
}