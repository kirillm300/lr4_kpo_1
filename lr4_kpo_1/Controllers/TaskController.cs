using lr4_kpo_1.Services;
using Microsoft.AspNetCore.Mvc;
using lr4_kpo_1.Models;
using lr4_kpo_1.Services;
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
            if (ModelState.IsValid)
            {
                try
                {
                    _taskService.AddTask(task);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ошибка при добавлении задания.");
                }
            }
            ViewBag.Courses = _courseService.GetCourses();
            return View(task);
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

        public IActionResult Delete(int id)
        {
            var task = _taskService.GetTask(id);
            if (task == null) return NotFound();
            return View(task);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _taskService.RemoveTask(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ошибка при удалении задания.");
                return View(_taskService.GetTask(id));
            }
        }
    }
}