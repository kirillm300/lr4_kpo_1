namespace lr4_kpo_1.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ProfessorName { get; set; }

        public Course(int id, string name, string? description, string professorName)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.ProfessorName = professorName;
        }
    }
}
