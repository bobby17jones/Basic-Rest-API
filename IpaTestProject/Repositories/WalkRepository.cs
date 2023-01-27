using IpaTestProject.Models.Domain;
using IpaTestProject.Data;
using Microsoft.EntityFrameworkCore;

namespace IpaTestProject.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly IpaTestProjectDbContext _context;

        public WalkRepository(IpaTestProjectDbContext context)
        {
            this._context = context;
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();

            await _context.AddAsync(walk);

            await _context.SaveChangesAsync();

            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            var walks = await _context.Walks
                    .Include(w => w.Region)
                    .Include(w => w.WalkDifficulty)
                    .ToListAsync();

            return walks;
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            var walk = await _context.Walks
                    .Include(w => w.Region)
                    .Include(w => w.WalkDifficulty)
                    .FirstOrDefaultAsync(w => w.Id == id);

            return walk;
        }

        public async Task<Walk> UpdateWalk(Guid id, Walk walk)
        {
            var existingWalk = await _context.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            existingWalk.Name = walk.Name;
            existingWalk.Length = walk.Length;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkDifficultyId = walk.WalkDifficultyId;

            await _context.SaveChangesAsync();

            return existingWalk;
        }

        public async Task<Walk> DeleteWalk(Guid id)
        {
            var existingWalk = await _context.Walks.FirstOrDefaultAsync(w => w.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            _context.Walks.Remove(existingWalk);

            await _context.SaveChangesAsync();

            return existingWalk;
        }
    }
}
