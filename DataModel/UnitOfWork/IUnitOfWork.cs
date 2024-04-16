using Domain.IRepository;

namespace UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository Generic { get; }

    IAssociationRepository AssociationRepository { get; }

    Task<int> CompleteAsync();
}
