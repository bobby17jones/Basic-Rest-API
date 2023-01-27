using IpaTestProject.Models.Domain;
using IpaTestProject.Data;
using Microsoft.EntityFrameworkCore;

namespace IpaTestProject.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly IpaTestProjectDbContext _context;

        public WalkDifficultyRepository(IpaTestProjectDbContext context)
        {
            this._context = context;
        }

        public async Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();

            await _context.AddAsync(walkDifficulty);

            await _context.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteWalkDifficulty(Guid id)
        {
            var existingWalkDifficulty = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            _context.Remove(existingWalkDifficulty);

            await _context.SaveChangesAsync();

            return existingWalkDifficulty;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
        {
            var walkDifficulties = await _context.WalkDifficulty.ToListAsync();

            return walkDifficulties;
        }

        public async Task<WalkDifficulty> GetWalkDifficultyById(Guid id)
        {
            var walkDifficulty = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);

            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _context.WalkDifficulty.FirstOrDefaultAsync(wd => wd.Id == id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;

            await _context.SaveChangesAsync();

            return existingWalkDifficulty;
        }
    }
}
