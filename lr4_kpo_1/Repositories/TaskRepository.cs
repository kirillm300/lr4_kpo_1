using lr4_kpo_1.Models;
using Task = lr4_kpo_1.Models.Task;

namespace lr4_kpo_1.Repositories
{
    public class TaskRepository
    {
        private List<Task> _tasks = new List<Task>();

        public List<Task> GetTasks() => _tasks;

        public void AddTask(Task task)
        {
            task.Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
            _tasks.Add(task);
        }

        public void UpdateTask(Task task)
        {
            var existing = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existing != null)
            {
                existing.Title = task.Title;
                existing.Description = task.Description;
                existing.CourseId = task.CourseId;
            }
        }

        public void RemoveTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null) _tasks.Remove(task);
        }

        public Task GetTask(int id) => _tasks.FirstOrDefault(t => t.Id == id);
    }
}