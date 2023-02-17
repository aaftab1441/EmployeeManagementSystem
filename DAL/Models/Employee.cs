
namespace DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? DepartmentId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Department Department { get; set; }
    }
}
