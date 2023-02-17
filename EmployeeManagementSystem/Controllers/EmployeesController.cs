using AutoMapper;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreMvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeManagementSystemContext _context;
        private readonly IEmployeeRepository _employeeRepos;
        private readonly IGenericRepository<Department> _departmentRepos;
        private readonly IMapper _mapper;

        public EmployeesController(IMapper mapper, 
                                   EmployeeManagementSystemContext context, 
                                   IEmployeeRepository employeeRepos,
                                   IGenericRepository<Department> departmentRepos)
        {
            _mapper = mapper;
            _context = context;
            _employeeRepos = employeeRepos;
            _departmentRepos = departmentRepos;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeRepos.GetAllEmployeesBySearchAsync("");
            var employeeViewModels = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepos.GetByIdAsync(e => e.Id == id, e => e.Department);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentRepos.GetAllAsync();
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                await _employeeRepos.InsertAsync(employee);
                await _employeeRepos.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentRepos.GetAllAsync();
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", employeeViewModel.DepartmentId);
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepos.GetByIdAsync(e => e.Id == id, e => e.Department);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            var departments = await _departmentRepos.GetAllAsync();
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", employeeViewModel.DepartmentId);
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel employeeViewModel)
        {
            if (id != employeeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(employeeViewModel);
                try
                {
                    _employeeRepos.Update(employee);
                    await _employeeRepos.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentRepos.GetAllAsync();
            ViewData["DepartmentId"] = new SelectList(departments, "Id", "Name", employeeViewModel.DepartmentId);
            return View(employeeViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepos.GetByIdAsync(e => e.Id == id, e => e.Department);
            if (employee == null)
            {
                return NotFound();
            }

            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(employeeViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _employeeRepos.Delete(e => e.Id == id);
            await _employeeRepos.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _employeeRepos.GetByIdAsync(e => e.Id == id) != null;
        }
    }
}
