using System.Collections.Generic;

namespace EmployeeDataService
{
    public interface IEmployeeDataService
    {
        void Add(Employee emp, Salary salary);

        List<Employee> GetEmployees();
        Employee? GetById(string empId);

        void Update(Employee emp, Salary salary);

        void Delete(string empId);
    }
}