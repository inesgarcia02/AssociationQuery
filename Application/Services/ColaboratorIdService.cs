
using Domain.IRepository;

namespace Application.Services
{
    public class ColaboratorIdService
    {

        private readonly IColaboratorsIdRepository _colaboratorsIdRepository;
        
        public ColaboratorIdService(IColaboratorsIdRepository colaboratorsIdRepository) {
            _colaboratorsIdRepository = colaboratorsIdRepository;
        }

        public async Task<long> Add(long colabId)
        {

            return await _colaboratorsIdRepository.Add(colabId);
        }
    }
}