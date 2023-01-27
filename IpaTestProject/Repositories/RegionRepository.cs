using IpaTestProject.Data;
using IpaTestProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IpaTestProject.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly IpaTestProjectDbContext _context;
        public RegionRepository(IpaTestProjectDbContext context)
        {
            this._context = context;
        }

        public async Task<Region> AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();

            await _context.AddAsync(region);

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteRegion(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (region == null)
            {
                return null;
            }

            _context.Regions.Remove(region);

            await _context.SaveChangesAsync();

            return region;
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionById(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> UpdateRegion(Guid id, Region region)
        {
           var existingRegion = await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (existingRegion == null)
            {
                return null;
            }

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            await _context.SaveChangesAsync();

            return existingRegion;
        }
    }
}
