using lr4_kpo_1.Data;
using Microsoft.EntityFrameworkCore;
using lr4_kpo_1.Models;
using System.Collections.Generic;
using System.Linq;
using Task = lr4_kpo_1.Models.Task;

namespace lr4_kpo_1.Repositories
{
    public class TaskRepository
    {
        private readonly StudyTrackerContext _context;

        public TaskRepository(StudyTrackerContext context)
        {
            _context = context;
        }

        public List<Task> GetTasks()
        {
            return _context.Tasks.Include(t => t.Course).ToList();
        }

        public void AddTask(Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public Task GetTaskById(int id)
        {
            return _context.Tasks
                .Include(t => t.Course) // Загружаем связанный курс
                .FirstOrDefault(t => t.Id == id);
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public void RemoveTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }

        public Task GetTask(int id)
        {
            return _context.Tasks.Include(t => t.Course).FirstOrDefault(t => t.Id == id);
        }
    }
}