using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovestraTodo.Application.DTOs
{
    public class TodoDto
    {

        public int Id { get; set; }
        public string Todo { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public bool IsRemoved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; } = null;
        // Navigation property to link to the user who created the todo
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
