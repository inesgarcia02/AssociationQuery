namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Domain.IRepository;

public class AssociationService
{
    private readonly IAssociationRepository _associationRepository;
    private readonly IColaboratorsIdRepository _colaboratorsRepository;
    private readonly IProjectRepository _projectRepository;

    public AssociationService(IAssociationRepository associationRepository, IColaboratorsIdRepository colaboratorsRepository, IProjectRepository projectRepository)
    {
        _associationRepository = associationRepository;
        _colaboratorsRepository = colaboratorsRepository;
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<AssociationDTO>> GetAll()
    {
        IEnumerable<Association> association = await _associationRepository.GetAssociationsAsync();

        IEnumerable<AssociationDTO> associationsDTO = AssociationDTO.ToDTO(association);

        return associationsDTO;
    }

    public async Task<AssociationDTO> GetById(long id)
    {
        Association association = await _associationRepository.GetAssociationsByIdAsync(id);

        if (association != null)
        {
            AssociationDTO associationDTO = AssociationDTO.ToDTO(association);

            return associationDTO;
        }
        return null;
    }

    public async Task<AssociationDTO> Add(AssociationDTO associationDTO, List<string> errorMessages)
    {
        bool exists = VerifyAssociation(associationDTO, errorMessages).Result;

        if (!exists)
        {
            return null;
        }

        try
        {
            Association association = AssociationDTO.ToDomain(associationDTO);

            Association associationSaved = await _associationRepository.Add(association);

            AssociationDTO assoDTO = AssociationDTO.ToDTO(associationSaved);

            string associationAmqpDTO = AssociationAmqpDTO.Serialize(assoDTO);
            //_associationAmqpGateway.Publish(associationAmqpDTO);

            return assoDTO;
        }
        catch (ArgumentException ex)
        {
            errorMessages.Add(ex.Message);
            return null;
        }
    }

    private async Task<bool> VerifyAssociation(AssociationDTO associationDTO, List<string> errorMessages)
    {
        bool aExists = await _associationRepository.AssociationExists(associationDTO.Id);
        if (aExists)
        {
            Console.WriteLine("Association already exists.");
            errorMessages.Add("Association already exists.");
            return false;
        }

        bool colabExists = await _colaboratorsRepository.ColaboratorExists(associationDTO.ColaboratorId);
        if (!colabExists)
        {
            Console.WriteLine("Colaborator doesn't exist.");
            errorMessages.Add("Colaborator doesn't exist.");
            return false;
        }

        bool projectExists = await _projectRepository.ProjectExists(associationDTO.ProjectId);
        if (!projectExists)
        {
            Console.WriteLine("Project doesn't exist.");
            errorMessages.Add("Project doesn't exist.");
            return false;
        }

        if (!CheckDates(associationDTO).Result)
        {
            Console.WriteLine("Association dates don't match with project.");
            errorMessages.Add("Association dates don't match with project.");
            return false;
        }

        return true;
    }


    private async Task<bool> CheckDates(AssociationDTO associationDTO)
    {
        Project p = await _projectRepository.GetProjectsByIdAsync(associationDTO.ProjectId);

        DateOnly startProject = p.StartDate;
        DateOnly? endProject = p.EndDate;

        DateOnly startAssociation = associationDTO.StartDate;
        DateOnly endAssociation = associationDTO.EndDate;

        if (endProject != null)
        {
            if (startAssociation >= startProject && endAssociation <= endProject)
            {
                return true;
            }
        }
        else if (startAssociation >= startProject)
        {
            return true;
        }


        return false;
    }


    // public async Task<bool> Update(long id, AssociationDTO AssociacaoDTO, List<string> errorMessages)
    // {
    //     Holiday holiday = await _associacaoRepository.GetHolidayByIdAsync(id);

    //     if(holiday!=null)
    //     {
    //         AssociationDTO.UpdateToDomain(holiday, AssociacaoDTO);

    //         await _associacaoRepository.Update(holiday, errorMessages);

    //         return true;
    //     }
    //     else
    //     {
    //         errorMessages.Add("Not found");

    //         return false;
    //     }
    // }
}
