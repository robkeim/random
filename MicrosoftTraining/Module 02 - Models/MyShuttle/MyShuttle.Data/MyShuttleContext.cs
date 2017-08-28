namespace MyShuttle.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;

    using Model;
    using System.Data.Entity;

    public class MyShuttleContext : IdentityDbContext<ApplicationUser>
    {
        public MyShuttleContext(string nameOrConnectionString = "DefaultConnection") : base(nameOrConnectionString, throwIfV1Schema: false)
        { }
        
        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Customer>().HasKey(c => c.CustomerId);
            builder.Entity<Carrier>().HasKey(c => c.CarrierId);
            builder.Entity<Employee>().HasKey(e => e.EmployeeId);
            builder.Entity<Vehicle>().HasKey(v => v.VehicleId);
            builder.Entity<Driver>().HasKey(d => d.DriverId);

            builder.Entity<Ride>()
                .HasRequired(i => i.Driver)
                .WithMany(i => i.Rides)
                .HasForeignKey(i => i.DriverId)
                .WillCascadeOnDelete(false);

            builder.Entity<Ride>()
              .HasRequired(i => i.Vehicle)
              .WithMany(i => i.Rides)
              .HasForeignKey(i => i.VehicleId)
              .WillCascadeOnDelete(false);

            //one - to - one described in EF fluent API is basically two one - to - many for both sides of the relationship,
            //which boils that to one - to - one when db is finally created

            builder.Entity<Driver>()
                .HasOptional(d => d.Vehicle).WithMany()
                .HasForeignKey(x => x.VehicleId);

            builder.Entity<Vehicle>()
            .HasRequired(x => x.Driver).WithMany()
            .HasForeignKey(x => x.DriverId).WillCascadeOnDelete(false);


            base.OnModelCreating(builder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Ride> Rides { get; set; }
    }
}


