using IpaTestProject.Models.Domain;

namespace IpaTestProject.Repositories
{
    public interface IRegionRepository
    {
       Task<IEnumerable<Region>> GetAll();
    }
}
