using DAL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Entities
{
    public class PlanesContext  : DbContext

    {
        protected readonly IConfiguration Configuration;

        public PlanesContext()
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Data Source= localhost; Database = PlanesDB; integrated security = True;");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Planes>().ToTable("Planes");
            modelBuilder.Entity<Maintanance>().ToTable("Maintanance");

            //İlk Planes Model'in ismi, ikincisi SQL Table ismi
        }

        public DbSet<Planes> Planes { get; set; }
        public DbSet<Maintanance> Maintanance { get; set; }

    }


}