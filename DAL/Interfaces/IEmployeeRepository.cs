using DAL.Models;

namespace DAL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetAllEmployeesBySearchAsync(string search);
    }
}
