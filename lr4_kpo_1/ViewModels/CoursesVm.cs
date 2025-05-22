using lr4_kpo_1.Models;

namespace lr4_kpo_1.ViewModels
{
    public class CoursesVm
    {
        public List<Course> Courses { get; set; }

        public CoursesVm(List<Course> cources)
        {
            this.Courses = cources;
        }
    }

}
