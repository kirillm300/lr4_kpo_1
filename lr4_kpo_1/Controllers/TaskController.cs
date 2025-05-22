using lr4_kpo_1.Services;
using Microsoft.AspNetCore.Mvc;
using lr4_kpo_1.Models;
using lr4_kpo_1.Services;
using System;
using Task = lr4_kpo_1.Models.Task;
using TaskStatus = lr4_kpo_1.Models.TaskStatus;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Index(TaskStatus? status = null, SortOrder sortOrder = SortOrder.None)
        {
            System.Diagnostics.Debug.WriteLine($"Task Index called with status: {status}, sortOrder: {sortOrder}");
            var tasks = _taskService.GetTasks() ?? new List<Task>();

            // Фильтрация по статусу
            if (status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == status.Value).ToList();
            }

            // Сортировка по дедлайну
            switch (sortOrder)
            {
                case SortOrder.DeadlineAsc:
                    tasks = tasks.OrderBy(t => t.Deadline).ToList();
                    break;
                case SortOrder.DeadlineDesc:
                    tasks = tasks.OrderByDescending(t => t.Deadline).ToList();
                    break;
                case SortOrder.None:
                default:
                    // Без сортировки, сохраняем порядок из базы данных
                    break;
            }

            var viewModel = new TaskIndexViewModel
            {
                Tasks = tasks,
                SelectedStatus = status,
                SelectedSortOrder = sortOrder
            };

            // Подготовка SelectList для статусов
            var statusList = Enum.GetValues(typeof(TaskStatus))
                .Cast<TaskStatus>()
                .Select(s => new SelectListItem
                {
                    Value = s.ToString(),
                    Text = s.ToString()
                })
                .ToList();
            statusList.Insert(0, new SelectListItem { Value = "", Text = "Все" });
            ViewBag.StatusList = new SelectList(statusList, "Value", "Text", status?.ToString());

            // Подготовка SelectList для сортировки
            var sortOrderList = Enum.GetValues(typeof(SortOrder))
                .Cast<SortOrder>()
                .Select(s => new SelectListItem
                {
                    Value = s.ToString(),
                    Text = s switch
                    {
                        SortOrder.None => "Без сортировки",
                        SortOrder.DeadlineAsc => "По возрастанию дедлайна",
                        SortOrder.DeadlineDesc => "По убыванию дедлайна",
                        _ => s.ToString()
                    }
                })
                .ToList();
            ViewBag.SortOrderList = new SelectList(sortOrderList, "Value", "Text", sortOrder.ToString());

            return View(viewModel);
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