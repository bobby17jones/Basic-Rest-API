using AutoMapper;
using IpaTestProject.Models.Domain;
using IpaTestProject.Models.DTO;
using IpaTestProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IpaTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            _walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWalks()
        {
            var walks = await _walkRepository.GetAllWalks();

            var walksDto = mapper.Map<List<Models.DTO.Walk>>(walks);

            return Ok(walksDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkById")]
        public async Task<ActionResult> GetWalkById(Guid id)
        {
            var walk = await _walkRepository.GetWalkById(id);

            if (walk == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<Models.DTO.Walk>(walk);

            return Ok(walkDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddWalk(AddWalk addWalkDto)
        {
            var walkDomain = new Models.Domain.Walk
            {
                Length = addWalkDto.Length,
                Name = addWalkDto.Name,
                RegionId = addWalkDto.RegionId,
                WalkDifficultyId = addWalkDto.WalkDifficultyId
            };

            var walk = await _walkRepository.AddWalk(walkDomain);

            var walkDto = mapper.Map<Models.DTO.Walk>(walk);

            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateWalk(Guid id, UpdateWalk updateWalkDto)
        {
            var walk = await _walkRepository.GetWalkById(id);

            if (walk == null)
            {
                return NotFound();
            }

            walk.Name = updateWalkDto.Name;
            walk.Length = updateWalkDto.Length;
            walk.RegionId = updateWalkDto.RegionId;
            walk.WalkDifficultyId = updateWalkDto.WalkDifficultyId;

            var updatedWalk = await _walkRepository.UpdateWalk(id, walk);

            var walkDto = mapper.Map<Models.DTO.Walk>(updatedWalk);

            return Ok(walkDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteWalk(Guid id)
        {
            var walk = await _walkRepository.GetWalkById(id);

            if (walk == null)
            {
                return NotFound();
            }

            await _walkRepository.DeleteWalk(id);

            return NoContent();
        }
    }
}
