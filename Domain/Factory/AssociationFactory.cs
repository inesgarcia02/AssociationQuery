using Domain.Model;

namespace Domain.Factory
{
    public class AssociationFactory : IAssociationFactory
    {
        public Association NewAssociation(long associationId, long colaboratorId, long projectId, DateOnly periodStart, DateOnly periodEnd, bool fundamental, bool isPendent)
        {

            return new Association(associationId, colaboratorId, projectId, periodStart, periodEnd, fundamental, isPendent);
        }
    }
}