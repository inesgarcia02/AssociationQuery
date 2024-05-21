using Domain.Model;

namespace Domain.IRepository;

public interface IAssociationRepository : IGenericRepository<Association>
{
    Task<IEnumerable<Association>> GetAssociationsAsync();
    Task<Association> GetAssociationsByIdAsync(long id);
    Task<Association> Add(Association association);
    Task<bool> AssociationExists(Association association);
    Task<IEnumerable<Association>> GetAssociationsByColabIdInPeriodAsync(long colabId, DateOnly startDate, DateOnly endDate);
    Task<IEnumerable<Association>> GetAssociationsByColabIdAsync(long id);
    Task<Association> Update(Association association);
    Task<IEnumerable<Association>> GetAssociationsByProjectIdAsync(long id);
}