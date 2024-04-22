namespace DataModel.Mapper;

using DataModel.Model;

using Domain.Model;
using Domain.Factory;
using Domain.IRepository;

public class AssociationMapper
{
    private IAssociationFactory _associationFactory;
    private IColaboratorsIdRepository _colaboratorRepository;
    private IProjectRepository _projectRepository;

    public AssociationMapper(IAssociationFactory associationFactory, IColaboratorsIdRepository colaboratorRepository, IProjectRepository projectRepository)
    {
        _associationFactory = associationFactory;
        _colaboratorRepository = colaboratorRepository;
        _projectRepository = projectRepository;
    }

    public Association ToDomain(AssociationDataModel associationDM)
    {
        Association associationDomain = _associationFactory.NewAssociation(associationDM.ColaboratorId.Id, associationDM.Project.Id, 
                                                    associationDM.StartDate,associationDM.EndDate);
        associationDomain.Id = associationDM.Id;
        return associationDomain;
    }

    public IEnumerable<Association> ToDomain(IEnumerable<AssociationDataModel> associacoesDM)
    {
        List<Association> associationsDomain = new List<Association>();

        foreach (AssociationDataModel associationDM in associacoesDM)
        {
            Association associationDomain = ToDomain(associationDM);

            associationsDomain.Add(associationDomain);
        }

        return associationsDomain.AsEnumerable();
    }

    public AssociationDataModel ToDataModel(Association association, ProjectDataModel project, ColaboratorsIdDataModel colaborator)
    {
        AssociationDataModel associationDataModel = new AssociationDataModel(association, project, colaborator);

        return associationDataModel;
    }
}