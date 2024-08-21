namespace KidOfTheDayAPI.Dtos
{
    public class KidProfileDto
    {
        public KidProfileDto(int userId, string firstName, string lastName, int schedule)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Schedule = schedule;
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = "";
        public int Schedule { get; set; }
    }
}
