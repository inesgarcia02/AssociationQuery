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
    }
}