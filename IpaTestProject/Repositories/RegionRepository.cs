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

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> GetRegionById(Guid id)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
