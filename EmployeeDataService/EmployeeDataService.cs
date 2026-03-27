using System.Collections.Generic;

namespace EmployeeDataService
{
    public class EmployeeService
    {
        private IEmployeeDataService _dataService;

        public EmployeeService()
        {
            _dataService = new EmployeeDBData();   // Edit for changing default database (Json or DB)
        }

        public EmployeeService(IEmployeeDataService dataService)
        {
            _dataService = dataService;
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
