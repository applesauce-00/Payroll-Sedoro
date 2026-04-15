using System;
using System.Collections.Generic;

namespace EmployeeDataService
{
    public class EmployeeService
    {
        private readonly IEmployeeDataService _dataService;

        public EmployeeService()
        {
            _dataService = new EmployeeDBData(); // Edit for changing default database (Json or DB)
        }

        public EmployeeService(IEmployeeDataService dataService)
        {
            _dataService = dataService;
        }

        public void Add(Employee emp)
        {
            ValidateEmployee(emp);

            if (GetById(emp.EmpId) != null)
                throw new Exception("Employee ID already exists.");

            _dataService.Add(emp);
        }

        public List<Employee> GetEmployees()
        {
            return _dataService.GetEmployees();
        }

        public Employee GetById(string id)
        {
            return _dataService.GetById(id);
        }

        public void Update(Employee emp)
        {
            ValidateEmployee(emp);

            _dataService.Update(emp);
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

            if (emp.HourlyRate <= 0)
                throw new Exception("Invalid Hourly Rate.");
        }
    }
}
