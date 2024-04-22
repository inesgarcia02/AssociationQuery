namespace DataModel.Model;
using Domain.Model;

public class AssociationDataModel
{
    public long Id { get; set; }
    public ColaboratorsIdDataModel ColaboratorId { get; set; }
    public ProjectDataModel Project { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    public AssociationDataModel() { }

    public AssociationDataModel(Association association, ProjectDataModel project, ColaboratorsIdDataModel colaborator)
    {
        Id = association.Id;
        StartDate = association.StartDate;
        EndDate = association.EndDate;
        Project = project;
        ColaboratorId = colaborator;
    }
}