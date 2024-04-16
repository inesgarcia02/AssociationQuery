using Domain.Model;

namespace Domain.Factory
{
    public class AssociationFactory : IAssociationFactory
    {
        public Association NewAssociation(long colaboratorId, long projectId, DateOnly periodStart, DateOnly periodEnd)
        {

            return new Association(colaboratorId, projectId, periodStart, periodEnd);
        }
    }
}