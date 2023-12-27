using Microsoft.EntityFrameworkCore;
using WONDERLUST_PROJECT_APIs.Models.DbModels;
namespace WONDERLUST_PROJECT_APIs.Data
{
    using WONDERLUST_PROJECT_APIs.Models.DbModels;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Bus> Bus { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Enquiry> Enquiry { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<PackageBooking> PackageBooking { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Travellers> Travellers { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasKey(u => new { u.UserId });

            //builder.Entity<State>()
            //     .HasKey(s => new { s.StateId, s.CountryId });


            //builder.Entity<Event>()
            //    .HasKey(e => new { e.Id });

            //builder.Entity<UserEvent>()
            //    .HasKey(ue => new { ue.UserId, ue.EventId });

            //builder.Entity<UserEvent>()
            //    .HasOne(ue => ue.User)
            //    .WithMany(user => user.UserEvents)
            //    .HasForeignKey(u => u.UserId);

            //builder.Entity<UserEvent>()
            //    .HasOne(uc => uc.Event)
            //    .WithMany(ev => ev.UserEvents)
            //    .HasForeignKey(ev => ev.EventId);
        }





    }
}