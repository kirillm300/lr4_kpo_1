using System;
using System.ComponentModel.DataAnnotations;

namespace lr4_kpo_1.Models
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Название задания обязательно")]
        public string Title { get; set; }

        public string? Description { get; set; } // Description явно nullable

        [Required(ErrorMessage = "Выберите курс")]
        [Range(1, int.MaxValue, ErrorMessage = "Выберите действительный курс")]
        public int CourseId { get; set; }

        public virtual Course? Course { get; set; } // Course явно nullable

        [Required(ErrorMessage = "Укажите дедлайн")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Выберите статус")]
        public TaskStatus Status { get; set; }
    }
}