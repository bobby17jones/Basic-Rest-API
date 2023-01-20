using AutoMapper;
using IpaTestProject.Models.Domain;
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
            this._regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
          var regions = await _regionRepository.GetAll();

          //return DTO regions
          //var regionsDto = new List<Models.DTO.Region>();
          //regions.ToList().ForEach(region =>
          //{
          //    var regionDto = new Models.DTO.Region();
          //    {
          //        regionDto.Id = region.Id;
          //        regionDto.Name = region.Name;
          //        regionDto.Code = region.Code;
          //        regionDto.Area = region.Area;
          //        regionDto.Lat = region.Lat;
          //        regionDto.Long = region.Long;
          //        regionDto.Population = region.Population;
          //    }

          //    regionsDto.Add(regionDto);
          //});

          var regionsDto = mapper.Map<List<Models.DTO.Region>>(regions);

          return Ok(regionsDto);
        }
    }
}
