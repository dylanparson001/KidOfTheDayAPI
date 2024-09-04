using KidOfTheDayAPI.Dtos;
using KidOfTheDayAPI.Models;

namespace KidOfTheDayAPI.Interfaces
{
    public interface IKidProfileRepository
    {
        public Task<List<KidProfile>> GetKidsByUser(int userId);
        public Task<KidProfile> GetKidProfileById(int id);
        public Task AddKidProfile(KidProfileDto profile);
        public Task UpdateKidProfile(int id, int schedule);
        public Task DeleteKidProfile(int id);
    }
}
