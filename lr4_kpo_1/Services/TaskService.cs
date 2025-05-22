using lr4_kpo_1.Models;
using lr4_kpo_1.Repositories;
using Task = lr4_kpo_1.Models.Task;

namespace lr4_kpo_1.Services
{
    public class TaskService
    {
        private readonly TaskRepository _repository;

        public TaskService(TaskRepository repository)
        {
            _repository = repository;
        }

        public List<Task> GetTasks() => _repository.GetTasks();
        public void AddTask(Task task) => _repository.AddTask(task);
        public void UpdateTask(Task task) => _repository.UpdateTask(task);
        public void RemoveTask(int id) => _repository.RemoveTask(id);
        public Task GetTask(int id) => _repository.GetTask(id);
    }
}