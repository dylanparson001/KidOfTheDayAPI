using KidOfTheDayAPI.Controllers;
using KidOfTheDayAPI.Models;

namespace KidOfTheDayAPI.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetUserByUsernameAndPassword(string username, string password);
        public Task<User> GetUserByUsername(string username);
        public Task RegisterUser(string username, string password, string email, string firstName, string lastName);
    }
}
