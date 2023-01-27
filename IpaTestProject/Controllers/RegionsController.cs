using AutoMapper;
using IpaTestProject.Models.Domain;
using IpaTestProject.Models.DTO;
using IpaTestProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IpaTestProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper mapper;
        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            _regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
          var regions = await _regionRepository.GetAll();

          var regionsDto = mapper.Map<List<Models.DTO.Region>>(regions);

          return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegion")]
        public async Task<IActionResult> GetRegion(Guid id)
        {
            var region =await _regionRepository.GetRegionById(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion(AddRegion addRegion)
        {
            if (!ValidateAddRegion(addRegion))
            {
                return BadRequest(ModelState);
            }

            var region = new Models.Domain.Region()
            {
                Code = addRegion.Code,
                Area = addRegion.Area,
                Lat = addRegion.Lat,
                Long = addRegion.Long,
                Name = addRegion.Name,
                Population = addRegion.Population
            };

            var response = await _regionRepository.AddRegion(region);

            var regionDto = new Models.DTO.Region
            {
                Id = response.Id,
                Code = response.Code,
                Area = response.Area,
                Lat = response.Lat,
                Long = response.Long,
                Name = response.Name,
                Population = response.Population
            };

            return CreatedAtAction(nameof(GetRegion), new {id = regionDto.Id}, regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion(Guid id)
        {
            var region = await _regionRepository.DeleteRegion(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegion(Guid id, UpdateRegion updateRegion)
        {
            if (!ValidateUpdateRegion(updateRegion))
            {
                return BadRequest(ModelState);
            }

            var region = new Models.Domain.Region
            {
                Code = updateRegion.Code,
                Area = updateRegion.Area,
                Lat = updateRegion.Lat,
                Long = updateRegion.Long,
                Name = updateRegion.Name,
                Population = updateRegion.Population
            };

            var response = await _regionRepository.UpdateRegion(id, region);

            if (response == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<Models.DTO.Region>(response);

            return Ok(regionDto);
        }

        #region Private Methods

        private bool ValidateAddRegion(AddRegion addRegion)
        {
            if (addRegion == null)
            {
                ModelState.AddModelError("AddRegion", "AddRegion object is null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegion.Code))
            {
                ModelState.AddModelError(nameof(addRegion.Code), $"{nameof(addRegion.Code)} cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(addRegion.Name))
            {
                ModelState.AddModelError(nameof(addRegion.Name), $"{nameof(addRegion.Name)} cannot be null or empty");
            }

            if (addRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Area), $"{nameof(addRegion.Area)} cannot be less than or equal to 0");
            }

            if (addRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Lat), $"{nameof(addRegion.Lat)} cannot be less than or equal to 0");
            }

            if (addRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegion.Long), $"{nameof(addRegion.Long)} cannot be less than or equal to 0");
            }

            if (addRegion.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegion.Population), $"{nameof(addRegion.Population)} cannot be less than 0");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateRegion(UpdateRegion updateRegion)
        {
            if (updateRegion == null)
            {
                ModelState.AddModelError("UpdateRegion", "UpdateRegion object is null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegion.Code))
            {
                ModelState.AddModelError(nameof(updateRegion.Code), $"{nameof(updateRegion.Code)} cannot be null or empty");
            }

            if (string.IsNullOrWhiteSpace(updateRegion.Name))
            {
                ModelState.AddModelError(nameof(updateRegion.Name), $"{nameof(updateRegion.Name)} cannot be null or empty");
            }

            if (updateRegion.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Area), $"{nameof(updateRegion.Area)} cannot be less than or equal to 0");
            }

            if (updateRegion.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Lat), $"{nameof(updateRegion.Lat)} cannot be less than or equal to 0");
            }

            if (updateRegion.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Long), $"{nameof(updateRegion.Long)} cannot be less than or equal to 0");
            }

            if (updateRegion.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegion.Population), $"{nameof(updateRegion.Population)} cannot be less than 0");
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
