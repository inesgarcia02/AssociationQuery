using System.Text.Json;
using Newtonsoft.Json;

namespace Application.DTO
{
    public class AssociationAmqpDTO
    {
    public long Id { get; set; }
        public long AssociationId { get; set; }
        public long ColaboratorId { get; set; }
        public long ProjectId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool Fundamental { get; set; }
        public string Status { get; set; }
        public bool IsPendent { get; set; }

        public AssociationAmqpDTO() { }

        public AssociationAmqpDTO(long id, long associationId, long colabId, long projectId, DateOnly startDate, DateOnly endDate, bool fundamental, string status, bool isPendent)
        {
            Id = id;
            AssociationId = associationId;
            ColaboratorId = colabId;
            ProjectId = projectId;
            StartDate = startDate;
            EndDate = endDate;
            Fundamental = fundamental;
            Status = status;
            IsPendent = isPendent;
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

             static public AssociationDTO ToDTO(AssociationAmqpDTO assoAmqpDTO)
        {
            AssociationDTO associationDTO = new AssociationDTO(assoAmqpDTO.Id, assoAmqpDTO.AssociationId, assoAmqpDTO.ColaboratorId, assoAmqpDTO.ProjectId, assoAmqpDTO.StartDate, assoAmqpDTO.EndDate, assoAmqpDTO.Fundamental, assoAmqpDTO.IsPendent);

            return associationDTO;
        }

    }
}