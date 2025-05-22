using lr4_kpo_1.Models;
using lr4_kpo_1.Repositories;

namespace lr4_kpo_1.Services
{
    public class CourseService
    {
        private readonly CourseRepository _repository;

        public CourseService(CourseRepository courseRepository)
        {
            _repository = courseRepository;
        }

        public List<Course> GetCourses()
        {
            return _repository.GetCourses();
        }

        public void AddCourse(Course course)
        {
            _repository.AddCourse(course);
        }

        public void RemoveCourse(int id)
        {
            _repository.RemoveCourse(id);
        }
    }

}
