namespace KidOfTheDayAPI.Models
{
    public class KidProfile
    {
        public KidProfile()
        {
            
        }
        public KidProfile(int id, int userId, string firstName, string lastName, int schedule)
        {
            Id = id;
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Schedule = schedule;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Schedule { get; set; }

    }
}
