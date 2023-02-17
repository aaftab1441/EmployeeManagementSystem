using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            DepartmentId = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsDeleted { get; set; }
        public string? Department { get; set; }
    }
}
