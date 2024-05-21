using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssociationController : ControllerBase
    {
        private readonly AssociationService _associationService;

        List<string> _errorMessages = new List<string>();

        public AssociationController(AssociationService associationService)
        {
            _associationService = associationService;
        }

        // GET: api/Association
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssociationDTO>>> GetAssociations()
        {
            IEnumerable<AssociationDTO> associationsDTO = await _associationService.GetAll();

            return Ok(associationsDTO);
        }


        // GET: api/Association/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AssociationDTO>> GetAssociationById(long id)
        {
            var associationDTO = await _associationService.GetById(id);

            if (associationDTO == null)
            {
                return NotFound();
            }

            return Ok(associationDTO);
        }

        // GET: api/AssociationByColab/5
        [HttpGet("colaborator/{colaboratorId}")]
        public async Task<ActionResult<AssociationDTO>> GetAssociationByColabId(long colaboratorId)
        {
            var associationDTO = await _associationService.GetByColabId(colaboratorId);

            if (associationDTO == null)
            {
                return NotFound();
            }

            return Ok(associationDTO);
        }
        // GET: api/Association/ByProject/1
        [HttpGet("ByProject/{projectId}")]
        public async Task<ActionResult<IEnumerable<AssociationDTO>>> GetAssociationsByProjectId(long projectId)
        {
            IEnumerable<AssociationDTO> associationsDTO = await _associationService.GetByProjectId(projectId);
            Console.WriteLine("Fetching associations for projectId: " + projectId);

            if (associationsDTO == null)
            {
                return NotFound();
            }
            return Ok(associationsDTO);
        }
    }

}