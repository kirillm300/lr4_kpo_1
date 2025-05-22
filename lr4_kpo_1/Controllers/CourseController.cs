using lr4_kpo_1.Services;
using lr4_kpo_1.ViewModelBuilders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr4_kpo_1.Controllers
{
    public class CourseController : Controller
    {
        private readonly CoursesVmBuilder _coursesVmBuilder;
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService
            , CoursesVmBuilder coursesVmBuilder)
        {
            _coursesVmBuilder = coursesVmBuilder;
            _courseService = courseService;
        }

        // GET: CourseController
        public ActionResult Index()
        {
            var coursesVm = _coursesVmBuilder.GetCoursesVm();
            return View(coursesVm);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            _courseService.RemoveCourse(id);
            return RedirectToAction(nameof(Index));
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
