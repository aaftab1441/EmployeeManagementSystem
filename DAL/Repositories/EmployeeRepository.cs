using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeManagementSystemContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetAllEmployeesBySearchAsync(string search)
        {
            return await _context.Set<Employee>().Include(e => e.Department)
                .Where(e => e.Name.Contains(search) || e.Email.Contains(search))
                .ToListAsync();
        }
    }
}