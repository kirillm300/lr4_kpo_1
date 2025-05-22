using lr4_kpo_1.Models;

namespace lr4_kpo_1.Repositories
{
    public class CourseRepository
    {
        private static List<Course> _courses;
        public CourseRepository()
        {
            SetDefaultCourses();
        }

        public static void SetDefaultCourses()
        {
            var course1 = new Course(id: 1, "Math", "some description", "Ivanov");
            var course2 = new Course(id: 2, "History", "world history", "Petrov");
            var course3 = new Course(id: 3, "Computer Science", description: null, "Sidorov");

            _courses = [course1, course2, course3];
        }

        public List<Course> GetCourses()
        {
            return _courses;
        }

        public void AddCourse(Course course)
        {
            _courses.Add(course);
        }

        public void RemoveCourse(int id)
        {
            var course = _courses.Single(c => c.Id == id);
            _courses.Remove(course);
        }
    }
}
