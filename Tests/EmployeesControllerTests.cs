using AutoMapper;
using CoreMvc.Controllers;
using DAL.Interfaces;
using DAL.Models;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace Tests
{
    [TestClass]
    public class EmployeesControllerTests
    {
        private EmployeesController _controller;
        private Mock<IEmployeeRepository> _employeeRepoMock;
        private Mock<IGenericRepository<Department>> _departmentRepoMock;
        private IMapper _mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            _employeeRepoMock = new Mock<IEmployeeRepository>();
            _departmentRepoMock = new Mock<IGenericRepository<Department>>();

            // Configure mapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();

            _controller = new EmployeesController(_mapper, null, _employeeRepoMock.Object, _departmentRepoMock.Object);
        }

        [TestMethod]
        public async Task Index_Returns_ViewResult_With_EmployeeViewModels()
        {
            // Arrange
            var employees = new List<Employee>()
        {
            new Employee(),
            new Employee()
        };
            _employeeRepoMock.Setup(x => x.GetAllEmployeesBySearchAsync(It.IsAny<string>())).ReturnsAsync(employees);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<EmployeeViewModel>));
            var model = result.ViewData.Model as IEnumerable<EmployeeViewModel>;
            Assert.AreEqual(employees.Count, model.Count());
        }

        [TestMethod]
        public async Task Details_Returns_NotFound_When_Id_Is_Null()
        {
            // Act
            var result = await _controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_Returns_NotFound_When_Employee_Is_Null()
        {
            // Arrange
            _employeeRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<Expression<Func<Employee, bool>>>(), It.IsAny<Expression<Func<Employee, object>>[]>()))
                             .ReturnsAsync((Employee)null);

            // Act
            var result = await _controller.Details(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Create_Returns_ViewResult_With_Departments()
        {
            // Arrange
            var departments = new List<Department>()
            {
                new Department(),
                new Department()
            };
            _departmentRepoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(departments);

            // Act
            var result = await _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData["DepartmentId"], typeof(SelectList));
            var selectList = result.ViewData["DepartmentId"] as SelectList;
            Assert.AreEqual(departments.Count, selectList.Count());
        }
    }
}
