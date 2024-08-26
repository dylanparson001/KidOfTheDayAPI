namespace KidOfTheDayAPI.Dtos
{
    public class ResponsibilityDto
    {
        public int KidId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
