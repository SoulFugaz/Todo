using System;

namespace Todo.Entities
{
    public class TaskModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OwnerId { get; set; } = Guid.Empty; // <-- новый: владелец задачи
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = "General";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; } = false;
    }
}
