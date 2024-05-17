namespace Application.DTO;
using Domain.Model;

public class AssociationDTO
{
    public long Id { get; set; }
    public long AssociationId { get; set; }
    public long ColaboratorId { get; set; }
    public long ProjectId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool Fundamental { get; set; }


    public AssociationDTO() { }

    public AssociationDTO(long associationId, long colabId, long projectId, DateOnly startDate, DateOnly endDate, bool fundamental)
    {
        AssociationId = associationId;
        ColaboratorId = colabId;
        ProjectId = projectId;
        StartDate = startDate;
        EndDate = endDate;
        Fundamental = fundamental;
    }

    static public AssociationDTO ToDTO(Association association)
    {
        AssociationDTO associationDTO = new AssociationDTO(association.Id, association.ColaboratorId, association.ProjectId,
                                                             association.StartDate, association.EndDate, association.Fundamental);
        return associationDTO;
    }

    static public IEnumerable<AssociationDTO> ToDTO(IEnumerable<Association> associations)
    {
        List<AssociationDTO> associationsDTO = new List<AssociationDTO>();

        foreach (Association a in associations)
        {
            AssociationDTO associationDTO = ToDTO(a);

            associationsDTO.Add(associationDTO);
        }

        return associationsDTO;
    }

    static public Association ToDomain(AssociationDTO associationDTO)
    {
        Association association = new Association(associationDTO.AssociationId, associationDTO.ColaboratorId, associationDTO.ProjectId, associationDTO.StartDate,
                                        associationDTO.EndDate, associationDTO.Fundamental);

        return association;
    }
}