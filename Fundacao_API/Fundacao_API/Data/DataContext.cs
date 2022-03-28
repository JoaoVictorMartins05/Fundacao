using Microsoft.EntityFrameworkCore;


namespace Fundacao_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Fundacao> Fundacao { get; set; }
    }
}
