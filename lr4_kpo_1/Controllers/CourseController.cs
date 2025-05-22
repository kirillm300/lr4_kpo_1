using lr4_kpo_1.Models;
using lr4_kpo_1.Services;
using lr4_kpo_1.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace lr4_kpo_1.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            var courses = _courseService.GetCourses();
            return View(new CoursesVm(courses));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _courseService.AddCourse(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ошибка при добавлении курса.");
                }
            }
            return View(course);
        }

        public IActionResult Edit(int id)
        {
            var course = _courseService.GetCourse(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course course)
        {
            if (id != course.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _courseService.UpdateCourse(course);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ошибка при редактировании курса.");
                }
            }
            return View(course);
        }

        public IActionResult Delete(int id)
        {
            var course = _courseService.GetCourse(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _courseService.RemoveCourse(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ошибка при удалении курса.");
                return View(_courseService.GetCourse(id));
            }
        }
    }
}