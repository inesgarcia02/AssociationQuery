namespace DataModel.Model;

using System.ComponentModel.DataAnnotations;
using Domain.Model;

public class AssociationDataModel
{
    [Key]
    public long Id { get; set; }
    public long AssociationId { get; set; }
    public ColaboratorsIdDataModel ColaboratorId { get; set; }
    public ProjectDataModel Project { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool Fundamental { get; set; }
    public bool IsPendent { get; set; }
    
    public AssociationDataModel() { }

    public AssociationDataModel(Association association, ProjectDataModel project, ColaboratorsIdDataModel colaborator)
    {
        Id = association.Id;
        AssociationId = association.AssociationId;
        StartDate = association.StartDate;
        EndDate = association.EndDate;
        Project = project;
        ColaboratorId = colaborator;
        Fundamental = association.Fundamental;
        IsPendent = association.IsPendent;
    }
}