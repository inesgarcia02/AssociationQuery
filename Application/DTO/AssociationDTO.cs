namespace Application.DTO;
using Domain.Model;

public class AssociationDTO
{
    public long Id { get; set; }
    public long ColaboratorId { get; set; }
    public long ProjectId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }


    public AssociationDTO() { }

    public AssociationDTO(long id, long colabId, long projectId, DateOnly startDate, DateOnly endDate)
    {
        Id = id;
        ColaboratorId = colabId;
        ProjectId = projectId;
        StartDate = startDate;
        EndDate = endDate;
    }

    static public AssociationDTO ToDTO(Association association)
    {
        AssociationDTO associationDTO = new AssociationDTO(association.Id, association.ColaboratorId, association.ProjectId,
                                                             association.StartDate, association.EndDate);
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
        Association association = new Association(associationDTO.ColaboratorId, associationDTO.ProjectId, associationDTO.StartDate, associationDTO.EndDate);

        return association;
    }
}