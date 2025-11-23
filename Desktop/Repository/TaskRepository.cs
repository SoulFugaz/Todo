using System;
using System.Collections.Generic;
using System.Linq;
using Todo.Entities;

namespace Desktop.Repository
{
    public static class TaskRepository
    {
        private static readonly List<TaskModel> _tasks = new();

        // Все категории, которые есть у конкретного пользователя
        public static IEnumerable<string> GetCategoriesForUser(Guid ownerId)
        {
            return _tasks.Where(t => t.OwnerId == ownerId).Select(t => t.Category).Distinct();
        }

        public static IEnumerable<TaskModel> GetTasksByCategoryForUser(string category, Guid ownerId)
        {
            return _tasks.Where(t => t.Category == category && t.OwnerId == ownerId);
        }

        public static IEnumerable<TaskModel> GetTasksForUser(Guid ownerId)
        {
            return _tasks.Where(t => t.OwnerId == ownerId);
        }

        public static IEnumerable<TaskModel> GetCompletedTasksForUser(Guid ownerId)
        {
            return _tasks.Where(t => t.OwnerId == ownerId && t.IsDone);
        }

        public static void AddTask(TaskModel task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            _tasks.Add(task);
        }

        public static void RemoveTask(TaskModel task)
        {
            _tasks.Remove(task);
        }

        public static void MarkAsDone(TaskModel task)
        {
            if (task == null) return;
            task.IsDone = true;
        }

        public static void ClearAllForUser(Guid ownerId)
        {
            _tasks.RemoveAll(t => t.OwnerId == ownerId);
        }
    }
}
