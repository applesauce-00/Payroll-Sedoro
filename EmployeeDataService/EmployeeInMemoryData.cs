using System.Collections.Generic;
using System.Linq;

namespace EmployeeDataService
{
    public class EmployeeInMemoryData : IEmployeeDataService
    {
        public List<Employee> dummyEmployees = new List<Employee>();

        public EmployeeInMemoryData()
        {
            Employee emp = new Employee
            {
                EmpId = "kirby",
                EmpName = "Kirby T. Sedoro",
                EmpTitle = "Operation Manager",
                HourlyRate = 300,
                HoursWorked = 80,
                OverTime = 3,
                LeaveDays = 1,
                NetPay = 0
            };

            dummyEmployees.Add(emp);
        }

        public void Add(Employee emp)
        {
            dummyEmployees.Add(emp);
        }

        public Employee? GetById(string empId)
        {
            return dummyEmployees.FirstOrDefault(e => e.EmpId == empId);
        }

        public List<Employee> GetEmployees()
        {
            return dummyEmployees;
        }

        public void Update(Employee emp)
        {
            var existing = GetById(emp.EmpId);

            if (existing != null)
            {
                existing.EmpName = emp.EmpName;
                existing.EmpTitle = emp.EmpTitle;
                existing.HourlyRate = emp.HourlyRate;
                existing.HoursWorked = emp.HoursWorked;
                existing.OverTime = emp.OverTime;
                existing.LeaveDays = emp.LeaveDays;
                existing.NetPay = emp.NetPay;
            }
        }

        public void Delete(string empId)
        {
            var existing = GetById(empId);

            if (existing != null)
            {
                dummyEmployees.Remove(existing);
            }
        }
    }
}
