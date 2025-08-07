using System.Runtime.CompilerServices;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.CustomActionFilter;
using NZWalks.Data;
using NZWalks.Models;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(
            NZWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // Get Data from  the Database - Domain Model
                var regionsDomain = await regionRepository.GetAllAsync();

                logger.LogInformation($"Found {JsonSerializer.Serialize(regionsDomain)} regions");

                var regionsDto = mapper.Map<List<RegionsDto>>(regionsDomain);

                // return DTOs
                return Ok(regionsDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching regions");
                return StatusCode(500, "Internal server error");
            }


        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // get region domain model from the database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound($"Region with ID {id} not found");
            }

            // return DTO back to the client
            return Ok(mapper.Map<RegionsDto>(regionDomain));
        }

        // post to create a new region
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            // use the domain model to create a new region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model to DTO
            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

        }

        // Update an existing region
        // PUT: https://localhost:5001/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound($"Region with ID {id} not found");
            }

            // convert updated domain model to DTO
            var regionDto = mapper.Map<RegionsDto>(regionDomainModel);

            return Ok(regionDto);
        }


        // Delete a region
        // DELETE: https://localhost:5001/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound($"Region with ID {id} not found");
            }

            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok($"Successfully deleted the region with ID {id}"); // 204 No Content response
        }

        // Delete all regions
        // DELETE: https://localhost:5001/api/regions/all
        [HttpDelete]
        [Route("all")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<IActionResult> DeleteAll()
        {
            // Get all regions first to check if any exist
            var regions = await dbContext.Regions.ToListAsync();

            if (!regions.Any())
            {
                return Ok("No regions found to delete");
            }

            // Delete all regions
            dbContext.Regions.RemoveRange(regions);
            await dbContext.SaveChangesAsync();

            return Ok($"Successfully deleted {regions.Count} regions");
        }
    }
}


