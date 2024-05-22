using DataModel.Model;
using DataModel.Repository;
using Domain.Model;

namespace WebApi.IntegrationTests.Helpers;

public static class Utilities
{
    public static void InitializeDbForTests(AbsanteeContext db)
    {
        var colaborators = GetSeedingColaboratorsDataModel();
        var projects = GetSeedingProjectsDataModel();

        db.ColaboratorsId.AddRange(colaborators);
        db.SaveChanges();

        db.Projects.AddRange(projects);
        db.SaveChanges();

        db.Associations.AddRange(GetSeedingAssociationsDataModel(projects, colaborators));
        db.SaveChanges();
    }

    public static void ReinitializeDbForTests(AbsanteeContext db)
    {
        db.Associations.RemoveRange(db.Associations);
        db.ColaboratorsId.RemoveRange(db.ColaboratorsId);
        db.Projects.RemoveRange(db.Projects);
        InitializeDbForTests(db);
    }

    public static List<ColaboratorsIdDataModel> GetSeedingColaboratorsDataModel()
    {
        return new List<ColaboratorsIdDataModel>()
        {
            new ColaboratorsIdDataModel(1),
            new ColaboratorsIdDataModel(2),
        };
    }

    public static List<ProjectDataModel> GetSeedingProjectsDataModel()
    {
        return new List<ProjectDataModel>()
        {
            new ProjectDataModel(new Project(1, new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31))),
            new ProjectDataModel(new Project(2, new DateOnly(2024, 1, 1), new DateOnly(2024, 12, 31)))
        };
    }

    public static List<AssociationDataModel> GetSeedingAssociationsDataModel(List<ProjectDataModel> projects, List<ColaboratorsIdDataModel> colabs)
    {
        var association1 = new Association(1, 1, 1, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31), true, false);
        association1.Id = 1;

        var association2 = new Association(2, 1, 2, new DateOnly(2024, 2, 1), new DateOnly(2024, 3, 31), true, false);
        association2.Id = 2;

        var association3 = new Association(3, 2, 1, new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31), true, false);
        association3.Id = 3;

        return new List<AssociationDataModel>()
        {
            new AssociationDataModel(association1, projects[0], colabs[0]),
            new AssociationDataModel(association2, projects[1], colabs[0]),
            new AssociationDataModel(association3, projects[0], colabs[1]),
        };
    }
}
