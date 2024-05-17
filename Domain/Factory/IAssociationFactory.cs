using Domain.Model;

namespace Domain.Factory
{
    public interface IAssociationFactory
    {
       Association NewAssociation(long associationId, long colaboratorId, long projectId, DateOnly periodStart, DateOnly periodEnd, bool fundamental, bool isPendent);
    }
}