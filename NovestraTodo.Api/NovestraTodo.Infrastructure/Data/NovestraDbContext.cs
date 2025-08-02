using Microsoft.EntityFrameworkCore;
using NovestraTodo.Core.Entities;

namespace NovestraTodo.Infrastructure.Data
{
    public class NovestraDbContext(DbContextOptions<NovestraDbContext> options) : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TodoEntity> Todos { get; set; }
    }
}
