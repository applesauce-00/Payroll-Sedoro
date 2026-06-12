using System;
using System.Collections.Generic;

namespace EmployeeDataService
{
    public class EmployeeService
    {
        private readonly IEmployeeDataService _dataService;

        public EmployeeService(IEmployeeDataService dataService)
        {
            _dataService = dataService;
        }
        public void Add(Employee emp, Salary salary)
        {
            ValidateEmployee(emp);

            if (_dataService.GetById(emp.EmpId) != null)
                throw new Exception("Employee ID already exists.");

            _dataService.Add(emp, salary);
        }

        public List<Employee> GetEmployees()
        {
            return _dataService.GetEmployees();
        }

        public Employee GetById(string id)
        {
            return _dataService.GetById(id);
        }

        public void Update(Employee emp, Salary salary)
        {
            ValidateEmployee(emp);
            _dataService.Update(emp, salary);
        }

        public void Delete(string id)
        {
            _dataService.Delete(id);
        }

        private void ValidateEmployee(Employee emp)
        {
            if (string.IsNullOrWhiteSpace(emp.EmpId))
                throw new Exception("Employee ID required.");

            if (string.IsNullOrWhiteSpace(emp.EmpName))
                throw new Exception("Employee Name required.");

        }
    }
}