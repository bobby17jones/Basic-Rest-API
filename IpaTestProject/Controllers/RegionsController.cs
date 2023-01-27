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
    }
}
