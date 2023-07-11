using HealthDeclarationAPI.Entities;
using Microsoft.EntityFrameworkCore;
namespace HealthDeclarationAPI.DAO {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
        public DbSet<HealthDeclarationEntity> HealthDeclarations { get; set; }
        public DbSet<HealthDeclarationItemEntity> HealthDeclarationItems { get; set; }

    }
}
