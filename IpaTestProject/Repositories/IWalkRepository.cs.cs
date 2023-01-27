using IpaTestProject.Models.Domain;

namespace IpaTestProject.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalks();
        Task<Walk> GetWalkById(Guid id);
        Task<Walk> AddWalk(Walk walk);
        Task<Walk> UpdateWalk(Guid id,Walk walk);
        Task<Walk> DeleteWalk(Guid id);
    }
}
