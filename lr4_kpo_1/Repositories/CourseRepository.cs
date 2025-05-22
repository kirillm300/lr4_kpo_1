using lr4_kpo_1.Data;
using lr4_kpo_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace lr4_kpo_1.Repositories
{
    public class CourseRepository
    {
        private readonly StudyTrackerContext _context;

        public CourseRepository(StudyTrackerContext context)
        {
            _context = context;
        }

        public List<Course> GetCourses()
        {
            return _context.Courses.ToList();
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void RemoveCourse(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }

        public Course GetCourse(int id)
        {
            return _context.Courses.Find(id);
        }
    }
}