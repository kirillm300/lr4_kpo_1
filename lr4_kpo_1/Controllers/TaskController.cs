using lr4_kpo_1.Services;
using Microsoft.AspNetCore.Mvc;
using lr4_kpo_1.Models;
using lr4_kpo_1.Services;
using System;
using Task = lr4_kpo_1.Models.Task;

namespace lr4_kpo_1.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;
        private readonly CourseService _courseService;

        public TaskController(TaskService taskService, CourseService courseService)
        {
            _taskService = taskService;
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View(_taskService.GetTasks());
        }

        public IActionResult Create()
        {
            ViewBag.Courses = _courseService.GetCourses();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task task)
        {
            System.Diagnostics.Debug.WriteLine("Task Create POST called");
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                System.Diagnostics.Debug.WriteLine("Validation errors: " + string.Join(", ", errors));
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error);
                }
                ViewBag.Courses = _courseService.GetCourses();
                return View(task);
            }
            try
            {
                _taskService.AddTask(task);
                System.Diagnostics.Debug.WriteLine("Task added successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError("", $"Ошибка при добавлении задания: {ex.Message}");
                ViewBag.Courses = _courseService.GetCourses();
                return View(task);
            }
        }

        public IActionResult Edit(int id)
        {
            var task = _taskService.GetTask(id);
            if (task == null) return NotFound();
            ViewBag.Courses = _courseService.GetCourses();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Task task)
        {
            if (id != task.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _taskService.UpdateTask(task);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ошибка при редактировании задания.");
                }
            }
            ViewBag.Courses = _courseService.GetCourses();
            return View(task);
        }

        public IActionResult Delete(int? id)
        {
            System.Diagnostics.Debug.WriteLine("Task Delete GET called");
            if (id == null)
            {
                return NotFound();
            }

            var task = _taskService.GetTaskById(id.Value);
            if (task == null || task.Course == null)
            {
                return NotFound();
            }

            var courses = _courseService.GetCourses();
            System.Diagnostics.Debug.WriteLine($"Courses count: {courses?.Count ?? 0}");
            ViewBag.Courses = courses;
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            System.Diagnostics.Debug.WriteLine("Task Delete POST called");
            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }

                _taskService.DeleteTask(id);
                System.Diagnostics.Debug.WriteLine("Task deleted successfully");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
                ModelState.AddModelError("", $"Ошибка при удалении задания: {ex.Message}");
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    return NotFound();
                }
                ViewBag.Courses = _courseService.GetCourses();
                return View("Delete", task);
            }
        }
    }
}