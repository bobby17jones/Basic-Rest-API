using AutoMapper;
using IpaTestProject.Models.Domain;
using IpaTestProject.Models.DTO;
using IpaTestProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IpaTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWalkDifficulties()
        {
            var walkDifficulties = await _walkDifficultyRepository.GetAllWalkDifficulties();

            var walkDifficultiesDto = mapper.Map<List<Models.DTO.WalkDifficulty>>(walkDifficulties);

            return Ok(walkDifficultiesDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultyById")]
        public async Task<ActionResult> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyById(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            var walkDifficultyDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddWalkDifficulty(AddWalkDifficulty addWalkDifficulty)
        {
            if (!ValidateAddWalkDifficulty(addWalkDifficulty))
            {
                return BadRequest(ModelState);
            }

            var walkDifficultyDomain = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDifficulty.Code
            };

            var walkDifficulty = await _walkDifficultyRepository.AddWalkDifficulty(walkDifficultyDomain);

            var walkDifficultyDto = mapper.Map<Models.DTO.WalkDifficulty>(walkDifficulty);

            return CreatedAtAction(nameof(GetWalkDifficultyById), new {id = walkDifficultyDto.Id}, walkDifficultyDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult> UpdateWalkDifficulty(Guid id, UpdateWalkDifficulty updateWalkDifficultyDto)
        {
            if (!ValidateUpdateWalkDifficulty(updateWalkDifficultyDto))
            {
                return BadRequest(ModelState);
            }
            
            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyById(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            walkDifficulty.Code = updateWalkDifficultyDto.Code;

            var updateWalkDifficulty = await _walkDifficultyRepository.UpdateWalkDifficulty(id, walkDifficulty);

            var walkDifficultyDto = mapper.Map<Models.DTO.WalkDifficulty>(updateWalkDifficulty);

            return Ok(walkDifficultyDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult> DeleteWalkDifficulty(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyById(id);

            if (walkDifficulty == null)
            {
                return NotFound();
            }

            await _walkDifficultyRepository.DeleteWalkDifficulty(id);
            
            return NoContent();
        }

        #region private Methods

        private bool ValidateAddWalkDifficulty(AddWalkDifficulty addWalkDifficulty)
        {
            if (addWalkDifficulty == null)
            {
                ModelState.AddModelError("AddWalkDifficulty", "AddWalkDifficulty object is null");
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficulty.Code), $"{nameof(addWalkDifficulty.Code)} cannot be null or empty");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficulty(UpdateWalkDifficulty updateWalkDifficulty)
        {
            if (updateWalkDifficulty == null)
            {
                ModelState.AddModelError("AddWalkDifficulty", "AddWalkDifficulty object is null");
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficulty.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficulty.Code), $"{nameof(updateWalkDifficulty.Code)} cannot be null or empty");
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
