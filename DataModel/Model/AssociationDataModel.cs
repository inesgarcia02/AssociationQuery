namespace DataModel.Model;

using DataModel.Model;
using Domain.Model;

public class AssociationDataModel
{
    public long Id { get; set; }
    public ColaboratorsIdDataModel ColaboratorId { get; set; }
    public ProjectDataModel Project { get; set; }
    public PeriodDataModel? Period { get; set; }

    public AssociationDataModel() {}

    public AssociationDataModel(Association association, ProjectDataModel project, ColaboratorsIdDataModel colaborator)
    {
        Id = association.Id;
        Project = project;
        ColaboratorId = colaborator;

        Period = new PeriodDataModel(association.Period);
       
    }
}