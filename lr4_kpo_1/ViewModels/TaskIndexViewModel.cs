using System.Collections.Generic;
using lr4_kpo_1.Models;

namespace lr4_kpo_1.Models
{
    public class TaskIndexViewModel
    {
        public List<Task> Tasks { get; set; }
        public TaskStatus? SelectedStatus { get; set; } // Nullable для "Все"
        public SortOrder SelectedSortOrder { get; set; }
    }

    public enum SortOrder
    {
        None,
        DeadlineAsc,
        DeadlineDesc
    }
}