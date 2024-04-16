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
            AssociationDataModel associationDataModel = _associationMapper.ToDataModel(association);

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

    public async Task<Association> Update(Association association, List<string> errorMessages)
    {
        try {
            AssociationDataModel associationDataModel = await _context.Set<AssociationDataModel>()
                    .Include(a => a.Period)
                    .FirstAsync(a => a.Id==association.Id);

            _associationMapper.UpdateDataModel(associationDataModel, association);

            _context.Entry(associationDataModel).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return association;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await AssociationExists(association.Id))
            {
                errorMessages.Add("Not found");
                
                return null;
            }
            else
            {
                throw;
            }

            return null;
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