using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    // DbContext instance represents a session with the database and can be used to query and s ave instances of your entities.
    public class DataContext : DbContext
    {
        // type CTOR for shortcut 
        public DataContext(DbContextOptions options) : base(options) 
        {
        }

        // PROP 
        // <Value> use Domain. CMD + . 
        // Domain because entities are located there
        public DbSet<Value> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // configure entity to add seed data
            builder.Entity<Value>()
            .HasData(
                new Value { Id = 1, Name = "Value 101" },
                new Value { Id = 2, Name = "Value 102" },
                new Value { Id = 3, Name = "Value 103" }
            );
        }
    }
}
