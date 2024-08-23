namespace KidOfTheDayAPI.Models
{
    public class Perk
    {
        public int Id { get; set; }
        public int KidId { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }

    }
}
