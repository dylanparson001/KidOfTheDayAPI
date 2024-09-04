using Microsoft.AspNetCore.Identity;

namespace KidOfTheDayAPI.Models
{
    public class User
    {
        public User()
        {
        }

        public User(int id, string username, string firstName, string lastName, string passwordHash, string emailAddress, string role)
        {
            Id = id;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            PasswordHash = passwordHash;
            EmailAddress = emailAddress;
            Role = Role;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
    }
}
