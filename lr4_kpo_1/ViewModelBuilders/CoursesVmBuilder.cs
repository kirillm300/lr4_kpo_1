using lr4_kpo_1.Models;
using lr4_kpo_1.Services;
using lr4_kpo_1.ViewModels;

namespace lr4_kpo_1.ViewModelBuilders
{
    public class CoursesVmBuilder
    {
        private readonly CourseService _courseService;
        public CoursesVmBuilder(CourseService courseService)
        {
            _courseService = courseService;
        }

        public CoursesVm GetCoursesVm()
        {
            var courses = _courseService.GetCourses();
            return new CoursesVm(courses);
        }
    }
}
