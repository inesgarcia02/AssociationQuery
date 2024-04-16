using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IColaboratorsIdRepository
    {
        Task<IEnumerable<long>> GetColaboratorsIdAsync();

        Task<long> Add(long id);
        Task<bool> ColaboratorExists(long colabId);
    }
}