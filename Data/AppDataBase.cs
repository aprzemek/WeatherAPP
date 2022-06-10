using Microsoft.EntityFrameworkCore;
using WeatherApplication.Models;

namespace WeatherApplication.Data
{
    public class AppDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server =(localdb)\MSSQLLocalDB;Database=aspnet-WeatherApp1-FC8D5B57-A762-4523-8E00-18F4C6693C5D;Trusted_Connection=True");
        }

        public DbSet<Weather>? Weathers { get; set; }
    }
}
