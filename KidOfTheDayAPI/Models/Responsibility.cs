namespace KidOfTheDayAPI.Models
{
    public class Responsibility
    {
        public Responsibility(int id, int kidId, string title, string description, bool completed)
        {
            Id = id;
            KidId = kidId;
            Title = title;
            Description = description;
            Completed = completed;
        }

        public int Id { get; set; }
        public int KidId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
