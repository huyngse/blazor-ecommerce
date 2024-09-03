using BlazorEcommerceSharedLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BlazorEcommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var decimalProps = builder.Model
             .GetEntityTypes()
             .SelectMany(t => t.GetProperties())
             .Where(p => (Nullable.GetUnderlyingType(p.ClrType) ?? p.ClrType) == typeof(decimal));

            foreach (var property in decimalProps)
            {
                property.SetPrecision(18);
                property.SetScale(2);
            }
        }
        public DbSet<Product> Products { get; set; }
    }
}
