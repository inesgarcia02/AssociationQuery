using Domain.Model;
using Domain.IRepository;
using DataModel.Mapper;
using Microsoft.EntityFrameworkCore;
using DataModel.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DataModel.Repository;

public class AssociationRepository : GenericRepository<Association>, IAssociationRepository
{
   
    AssociationMapper _associationMapper;

    public AssociationRepository(AbsanteeContext context, AssociationMapper mapper) : base(context!)
    {
        _associationMapper = mapper;
    }

    public async Task<IEnumerable<Association>> GetAssociationsAsync()
    {
        try
        {
            IEnumerable<AssociationDataModel> associationsDataModel = await _context.Set<AssociationDataModel>()
                .Include(x => x.Period)
                    .ToListAsync();

            IEnumerable<Association> associations = _associationMapper.ToDomain(associationsDataModel);

            return associations;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Association> GetAssociationsByIdAsync(long id)
    {
        try
        {
            AssociationDataModel associationDataModel = await _context.Set<AssociationDataModel>()
            .Include(x => x.Period)
                .FirstAsync(a => a.Id == id);

            Association association = _associationMapper.ToDomain(associationDataModel);

            return association;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Association> Add(Association association)
    {
        try
        {
            ProjectDataModel projectDataModel = await _context.Set<ProjectDataModel>()
                .FirstAsync(p => p.Id == association.ProjectId);
            
            ColaboratorsIdDataModel colaboratorDataModel = await _context.Set<ColaboratorsIdDataModel>()
                .FirstAsync(c => c.Id == association.ColaboratorId);

            AssociationDataModel associationDataModel = _associationMapper.ToDataModel(association, projectDataModel, colaboratorDataModel);

            EntityEntry<AssociationDataModel> associationDataModelEntityEntry = _context.Set<AssociationDataModel>().Add(associationDataModel);

            await _context.SaveChangesAsync();


            AssociationDataModel associationDataModelSaved = associationDataModelEntityEntry.Entity;

            Association associationSaved = _associationMapper.ToDomain(associationDataModelSaved);

            return associationSaved;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> AssociationExists(long id)
    {
        return await _context.Set<AssociationDataModel>().AnyAsync(h => h.Id == id);
    }
}