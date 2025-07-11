using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilter;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    // /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        // CREATE Walk
        // post api/walks
        [HttpPost]
        [ValidateModel] // Custom action filter to validate the model
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto walk)
        {

            // map the DTO to the Domain Model
            var walkDomainModel = mapper.Map<Walk>(walk);

            await walkRepository.CreateAsync(walkDomainModel);

            // map the Domain Model back to the DTO
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);


        }

        // GET all walks
        // GET api/walks?filterOn=Name&filterQuery=Mount
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            // get all walks from the repository
            var walksDomain = await walkRepository.GetAllAsync(filterOn, filterQuery);

            // map the domain models to DTOs
            var walksDto = mapper.Map<List<WalkDto>>(walksDomain);

            // return the DTOs
            return Ok(walksDto);
        }


        // GET Walk by ID
        // GET api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound($"Walk with ID {id} not found");
            }

            // map the domain model to the DTO
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }


        // Update Walk
        // PUT api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            // map the DTO to the Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            // update the walk in the repository
            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound($"Walk with ID {id} not found");
            }

            // map the updated domain model to the DTO
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);
            return Ok(walkDto);
        }

        // Delete Walk
        // DELETE api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // delete the walk from the repository
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound($"Walk with ID {id} not found");
            }

            // return a success response
            return Ok($"Walk with ID {id} deleted successfully");
        }
    }
}
