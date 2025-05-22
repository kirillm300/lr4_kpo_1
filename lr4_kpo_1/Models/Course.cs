using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace lr4_kpo_1.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название курса обязательно")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "Укажите имя преподавателя")]
        public string ProfessorName { get; set; }

        // Навигационное свойство для заданий
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}