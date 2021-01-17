using Microsoft.EntityFrameworkCore;
using ProcessFileData.Model;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ProcessFileData.Context
{
    public class ProcessFileDataContext : DbContext
  {
    public DbSet<ProductData> ProductData { get; set; }
    public DbSet<TransmissionsummaryData> TransmissionsummaryData { get; set; }
    public DbSet<StatisticData> StatisticData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL("server=db;database=product;user=root;password=password;"          
        );      
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder); 
      modelBuilder.Entity<ProductData>(entity =>
      {
        entity.HasKey(p => p.sku);
        
      });    
       modelBuilder.Entity<TransmissionsummaryData>(entity =>
      {
        entity.HasKey(t => t.id);
        
      }); 
       modelBuilder.Entity<StatisticData>(entity =>
      {
        entity.HasNoKey().ToView(null);;
        
      }); 
    }
  }

}


