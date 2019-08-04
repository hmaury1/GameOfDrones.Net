using System.Data.Entity;
using GameOfDronesDataAccessLayer.DataAccess.Entities;

namespace GameOfDronesDataAccessLayer.DataAccess
{
    public class AppContext : DbContext
    {
        public AppContext(string connectName) : base(connectName)
        {
            Database.SetInitializer<AppContext>(null); //new CreateDatabaseIfNotExists<SkAppContext>());
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }
    }
}