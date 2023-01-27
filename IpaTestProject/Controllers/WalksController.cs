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
        private readonly IRegionRepository _regionRepository;
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultyRepository walkDifficultyRepository)
        {
            _walkRepository = walkRepository;
            this.mapper = mapper;
            _regionRepository = regionRepository;
            _walkDifficultyRepository = walkDifficultyRepository;
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
            if (!(await ValidateAddWalk(addWalkDto)))
            {
                return BadRequest(ModelState);
            }

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
            if (!(await ValidateUpdateWalk(updateWalkDto)))
            {
                return BadRequest(ModelState);
            }
            
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

        #region private methods

        private async Task<bool> ValidateAddWalk(AddWalk addWalkDto)
        {
            if (addWalkDto == null)
            {
                ModelState.AddModelError("AddWalk", "AddWalk object is null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDto.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }

            if (addWalkDto.Length <= 0)
            {
                ModelState.AddModelError("Length", "Length is required");
            }

            var region = await _regionRepository.GetRegionById(addWalkDto.RegionId);

            if (region == null)
            {
                ModelState.AddModelError("RegionId", "RegionId is required");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyById(addWalkDto.WalkDifficultyId);

            if (walkDifficulty == null)
            {
                ModelState.AddModelError("WalkDifficultyId", "WalkDifficultyId is required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalk(UpdateWalk updateWalk)
        {
            if (updateWalk == null)
            {
                ModelState.AddModelError("AddWalk", "AddWalk object is null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalk.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }

            if (updateWalk.Length <= 0)
            {
                ModelState.AddModelError("Length", "Length is required");
            }

            var region = await _regionRepository.GetRegionById(updateWalk.RegionId);

            if (region == null)
            {
                ModelState.AddModelError("RegionId", "RegionId is required");
            }

            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyById(updateWalk.WalkDifficultyId);

            if (walkDifficulty == null)
            {
                ModelState.AddModelError("WalkDifficultyId", "WalkDifficultyId is required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
