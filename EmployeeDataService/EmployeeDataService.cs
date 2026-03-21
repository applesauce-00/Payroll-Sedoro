using System.Collections.Generic;

namespace EmployeeDataService
{
    public class EmployeeService
    {
        IEmployeeDataService _dataService;

        public EmployeeService(IEmployeeDataService employeeDataService)
        {
            _dataService = employeeDataService;
        }

        public void Add(Employee emp)
        {
            _dataService.Add(emp);
        }

        public List<Employee> GetEmployees()
        {
            return _dataService.GetEmployees();
        }

        public Employee? GetById(string empId)
        {
            return _dataService.GetById(empId);
        }

        public void Update(Employee emp)
        {
            _dataService.Update(emp);
        }

        public void Delete(string empId)
        {
            _dataService.Delete(empId);
        }
    }
}
