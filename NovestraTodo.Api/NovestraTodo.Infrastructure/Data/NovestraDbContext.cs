using Microsoft.EntityFrameworkCore;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Infrastructure.Data
{
    public class NovestraDbContext : DbContext
    {
        public NovestraDbContext (DbContextOptions<NovestraDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<TodoEntity> Todos { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
