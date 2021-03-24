using Microsoft.EntityFrameworkCore;
using SpaceParkModel.Data;
using SpaceParkModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkModel
{
    public class SpaceParkContext : DbContext
    {
        public DbSet<Occupancy> Occupancies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<ParkingSize> ParkingSizes { get; set; }
        public DbSet<ParkingSpots> ParkingSpots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AFKC3I2\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=SpacePark");
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\SQLEXPRESS;Initial Catalog=SpacePark");
        }
    }
}
