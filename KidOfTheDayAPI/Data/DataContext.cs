using Microsoft.EntityFrameworkCore;

namespace KidOfTheDayAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    }
}
