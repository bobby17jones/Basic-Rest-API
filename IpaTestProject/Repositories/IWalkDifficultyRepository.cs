using IpaTestProject.Models.Domain;

namespace IpaTestProject.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties();
        Task<WalkDifficulty> GetWalkDifficultyById(Guid id);
        Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdateWalkDifficulty(Guid id, WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DeleteWalkDifficulty(Guid id);
    }
}
