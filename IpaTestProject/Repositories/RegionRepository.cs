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

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }
    }
}
