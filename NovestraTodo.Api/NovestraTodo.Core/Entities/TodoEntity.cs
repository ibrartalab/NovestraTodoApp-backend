
namespace NovestraTodo.Core.Entities
{
    public class TodoEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } = null;
        // Navigation property to link to the user who created the todo
        public Guid UserId { get; set; }
        public required UserEntity User { get; set; }
    }
}
