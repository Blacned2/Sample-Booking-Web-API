using KapitalMedyaBooking.Data.Models;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Linq;

namespace KapitalMedyaBooking.Data.Context
{
    public class KapitalDbContext:DbContext
    {
        public KapitalDbContext() : base()
        {

        }
        public KapitalDbContext(DbContextOptions<KapitalDbContext> options) : base(options)
        {

        }

        #region DBSets
        public DbSet<Appartment> Appartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            optionsBuilder.UseNpgsql("Server=psql-mock-database-cloud.postgres.database.azure.com;Port=5432;Database=booking1661587392445tgkywwjikcpzzmhb;User Id=fvtmsihhvwqiscdhffteysco@psql-mock-database-cloud;Password=xwcnlqvdlnxsjkagvemjqxvw;");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        //    {
        //        // EF Core 1 & 2
        //        //property.Relational().ColumnType = "decimal(18, 4)";

        //        // EF Core 3+
        //        property.SetColumnType("decimal(18, 4)");
        //    }

        //    foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
        //    {
        //        // EF Core 1 & 2
        //        //property.Relational().ColumnType = "datetime";

        //        // EF Core 3+
        //        property.SetColumnType("datetime");
        //    }

        //    // table naming
        //    foreach (var entity in modelBuilder.Model.GetEntityTypes())
        //    {
        //        // Replace table names
        //        //entity.Relational().TableName = entity.Relational().TableName.ToLowerCase();
        //        entity.SetTableName(entity.GetTableName().ToLower());

        //        // Replace column names            
        //        foreach (var property in entity.GetProperties())
        //        {
        //            //property.Relational().ColumnName = property.Relational().ColumnName.ToLowerCase();
        //            property.SetColumnName(property.Name.ToLower());
        //        }

        //        foreach (var key in entity.GetKeys())
        //        {
        //            key.SetName(key.GetName().ToLower());
        //        }

        //        foreach (var key in entity.GetForeignKeys())
        //        {
        //            key.PrincipalKey.SetName(key.PrincipalKey.GetName().ToLower());
        //            key.DeleteBehavior = DeleteBehavior.Restrict;
        //        }

        //        foreach (var index in entity.GetIndexes())
        //        {
        //            index.SetDatabaseName(index.GetDatabaseName().ToLower());
        //        }
        //    }
              
        //}
    }
}
