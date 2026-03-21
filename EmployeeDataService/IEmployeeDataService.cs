using System.Collections.Generic;

namespace EmployeeDataService
{
    public interface IEmployeeDataService
    {
        void Add(Employee emp);
        List<Employee> GetEmployees();
        Employee? GetById(string empId);
        void Update(Employee emp);
        void Delete(string empId);
    }
}
