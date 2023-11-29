using Microsoft.EntityFrameworkCore;

namespace DonorInfoAPI.models
{
    public class DonorDBContext:DbContext
    {
        public DonorDBContext(DbContextOptions<DonorDBContext>options):base(options) 
        {
            
        }
        public DbSet<DonorInfo>DonorInfo { get; set; }
    }
}
