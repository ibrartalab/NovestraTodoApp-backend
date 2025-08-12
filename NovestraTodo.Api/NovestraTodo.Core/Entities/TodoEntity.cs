
using System.ComponentModel.DataAnnotations.Schema;

namespace NovestraTodo.Core.Entities
{
    public class TodoEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Todo { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public bool IsRemoved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } = null;
        // Navigation property to link to the user who created the todo
        public Guid UserId { get; set; }
    }
}
