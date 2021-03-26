using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;

namespace SpaceParkModel.Database
{
    public class SpaceParkContext : DbContext
    {
        public DbSet<Occupancy> Occupancies { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Spaceship> Spaceships { get; set; }
        public DbSet<ParkingSize> ParkingSizes { get; set; }
        public DbSet<ParkingSpots> ParkingSpots { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //production: optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AFKC3I2\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=SpacePark");
            //testing: optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AFKC3I2\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=SpaceParkTesting");
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AFKC3I2\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=SpaceParkTesting");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ParkingSize
            modelBuilder.Entity<ParkingSize>().HasData(new ParkingSize{ ID = 1, Size = 1000, Price = 100.00M });
            modelBuilder.Entity<ParkingSize>().HasData(new ParkingSize{ ID = 2, Size = 10000, Price = 1000.00M });
            modelBuilder.Entity<ParkingSize>().HasData(new ParkingSize{ ID = 3, Size = 150000, Price = 5000.00M });
            #endregion

            #region ParkingSpots
            modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots { Spot = 1, ParkingSizeID = 1 });
            modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots { Spot = 2, ParkingSizeID = 1 });
            modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots { Spot = 3, ParkingSizeID = 1 });
            modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots { Spot = 4, ParkingSizeID = 2 });
            modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots { Spot = 5, ParkingSizeID = 2 });
            modelBuilder.Entity<ParkingSpots>().HasData(new ParkingSpots { Spot = 6, ParkingSizeID = 3 });
            #endregion

            #region Person
            modelBuilder.Entity<Person>().HasData(new Person { ID = 1, Name = "Luke Skywalker" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 2, Name = "R2-D2" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 3, Name = "Leia Organa" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 4, Name = "C-3PO" });
            modelBuilder.Entity<Person>().HasData(new Person { ID = 5, Name = "Beru Whitesun lars" });
            #endregion

            #region Spaceship
            modelBuilder.Entity<Spaceship>().HasData(new Spaceship { ID = 1, Name = "X-wing" });
            modelBuilder.Entity<Spaceship>().HasData(new Spaceship { ID = 2, Name = "Imperial shuttle" });
            modelBuilder.Entity<Spaceship>().HasData(new Spaceship { ID = 3, Name = "Republic Cruiser" });
            modelBuilder.Entity<Spaceship>().HasData(new Spaceship { ID = 4, Name = "Slave 1" });
            modelBuilder.Entity<Spaceship>().HasData(new Spaceship { ID = 5, Name = "Republic attack cruiser" });
            modelBuilder.Entity<Spaceship>().HasData(new Spaceship { ID = 6, Name = "J-type diplomatic barge" });
            #endregion

            #region Occupancy
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 1,  PersonID = 1, SpaceshipID = 2, ArrivalTime = DateTime.Parse("2021-03-22 18:26:53.6491508", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-22 19:08:00.3701198", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 2,  PersonID = 1, SpaceshipID = 2, ArrivalTime = DateTime.Parse("2021-03-22 19:25:37.8613124", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-22 19:35:09.1532960", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 3,  PersonID = 1, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-22 19:35:45.7119285", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-22 19:38:27.6691688", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 4,  PersonID = 1, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-22 19:39:23.3433465", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-22 19:39:27.3658201", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 5,  PersonID = 1, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-22 19:39:46.3596936", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-22 19:40:06.1989431", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 6,  PersonID = 1, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-22 19:41:28.5042097", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-22 19:41:30.4573860", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 7,  PersonID = 1, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-23 11:42:13.4526063", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-23 11:42:58.9026162", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 8,  PersonID = 2, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-24 12:15:59.0461350", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-24 12:16:12.7361570", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 9,  PersonID = 2, SpaceshipID = 3, ArrivalTime = DateTime.Parse("2021-03-24 12:17:55.9968849", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-24 12:18:02.4895235", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 10, PersonID = 2, SpaceshipID = 4, ArrivalTime = DateTime.Parse("2021-03-24 12:22:06.0277378", CultureInfo.InvariantCulture), DepartureTime = null,                                                                        ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 11, PersonID = 1, SpaceshipID = 5, ArrivalTime = DateTime.Parse("2021-03-24 12:22:24.5828001", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-24 14:43:18.8014517", CultureInfo.InvariantCulture), ParkingSpotID = 4 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 12, PersonID = 1, SpaceshipID = 4, ArrivalTime = DateTime.Parse("2021-03-25 10:01:58.6533626", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-25 10:59:42.1208154", CultureInfo.InvariantCulture), ParkingSpotID = 2 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 13, PersonID = 1, SpaceshipID = 6, ArrivalTime = DateTime.Parse("2021-03-25 10:59:56.0403378", CultureInfo.InvariantCulture), DepartureTime = DateTime.Parse("2021-03-25 11:00:07.0538153", CultureInfo.InvariantCulture), ParkingSpotID = 1 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 14, PersonID = 1, SpaceshipID = 1, ArrivalTime = DateTime.Parse("2021-03-24 12:22:06.0277378", CultureInfo.InvariantCulture), DepartureTime = null,                                                                        ParkingSpotID = 3 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 15, PersonID = 3, SpaceshipID = 2, ArrivalTime = DateTime.Parse("2021-03-24 12:22:06.0277378", CultureInfo.InvariantCulture), DepartureTime = null,                                                                        ParkingSpotID = 4 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 16, PersonID = 4, SpaceshipID = 3, ArrivalTime = DateTime.Parse("2021-03-24 12:22:06.0277378", CultureInfo.InvariantCulture), DepartureTime = null,                                                                        ParkingSpotID = 5 });
            modelBuilder.Entity<Occupancy>().HasData(new Occupancy { ID = 17, PersonID = 5, SpaceshipID = 5, ArrivalTime = DateTime.Parse("2021-03-24 12:22:06.0277378", CultureInfo.InvariantCulture), DepartureTime = null,                                                                        ParkingSpotID = 6 });
            #endregion

            #region Payment
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 1, OccupancyID = 1, Amount = 600.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 2, OccupancyID = 2, Amount = 100.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 3, OccupancyID = 3, Amount = 400.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 4, OccupancyID = 4, Amount = 10000.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 5, OccupancyID = 5, Amount = 850.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 6, OccupancyID = 6, Amount = 80.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 7, OccupancyID = 7, Amount = 1200.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 8, OccupancyID = 8, Amount = 250.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 9, OccupancyID = 9, Amount = 400.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 10, OccupancyID = 11, Amount = 700.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 11, OccupancyID = 12, Amount = 350.00M });
            modelBuilder.Entity<Payment>().HasData(new Payment { ID = 12, OccupancyID = 13, Amount = 300.00M });
            #endregion
        }
    }
}
