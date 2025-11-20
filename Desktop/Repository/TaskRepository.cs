using System.Collections.Generic;
using System.Linq;
using Todo.Entities;

namespace Desktop.Repository
{
    public static class TaskRepository
    {
        private static readonly List<TaskModel> _tasks = new();

        public static IEnumerable<string> GetCategories()
        {
            return _tasks.Select(t => t.Category).Distinct();
        }

        public static IEnumerable<TaskModel> GetTasksByCategory(string category)
        {
            return _tasks.Where(t => t.Category == category);
        }

        public static void AddTask(TaskModel task)
        {
            _tasks.Add(task);
        }

        public static void RemoveTask(TaskModel task)
        {
            _tasks.Remove(task);
        }

        public static void MarkAsDone(TaskModel task)
        {
            task.IsDone = true;
        }
    }
}