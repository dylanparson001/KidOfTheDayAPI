using KidOfTheDayAPI.Dtos;
using KidOfTheDayAPI.Models;

namespace KidOfTheDayAPI.Interfaces
{
    public interface IKidProfileRepository
    {
        public Task<List<KidProfile>> GetKidsByUser(int userId);
        public Task AddKidProfile(KidProfileDto profile);
    }
}
