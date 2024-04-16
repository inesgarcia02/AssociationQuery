namespace DataModel.Model;

using DataModel.Model;
using Domain.Model;

public class AssociationDataModel
{
    public long Id { get; set; }
    public long ColaboratorId { get; set; }
    public long ProjectId { get; set; }
    public PeriodDataModel? Period { get; set; }

    public AssociationDataModel() {}

    public AssociationDataModel(Association association)
    {
        Id = association.Id;
        ColaboratorId = association.ColaboratorId;
        ProjectId = association.ProjectId;

        Period = new PeriodDataModel(association.Period);
       
    }
}