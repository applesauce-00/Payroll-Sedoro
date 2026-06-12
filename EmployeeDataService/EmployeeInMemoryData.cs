using System.Collections.Generic;
using System.Linq;

namespace EmployeeDataService
{
    public class EmployeeInMemoryData : IEmployeeDataService
    {
        private List<Employee> _employees = new List<Employee>();
        private List<Salary> _salaries = new List<Salary>();

        public EmployeeInMemoryData()
        {
            // Adding seeds
            _employees.Add(new Employee { EmpId = "kirby", EmpName = "Kirby T. Sedoro", EmpTitle = "Operation Manager", Leave = 0 });
            _salaries.Add(new Salary { EmpId = "kirby", HoursWorked = 80, HourlyRate = 300, OverTimeHours = 3, OverTimePay = 0, NetPay = 0 });
        }

        public void Add(Employee emp, Salary salary)
        {
            _employees.Add(emp);
            _salaries.Add(salary);
        }

        public Employee GetById(string empId)
        {
            var emp = _employees.FirstOrDefault(e => e.EmpId == empId);
            if (emp != null)
            {
                emp.SalaryInfo = _salaries.FirstOrDefault(s => s.EmpId == empId);
            }
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            foreach (var emp in _employees)
            {
                emp.SalaryInfo = _salaries.FirstOrDefault(s => s.EmpId == emp.EmpId);
            }
            return _employees;
        }

        public void Update(Employee emp, Salary salary)
        {
            var existingEmp = _employees.FirstOrDefault(e => e.EmpId == emp.EmpId);
            var existingSal = _salaries.FirstOrDefault(s => s.EmpId == emp.EmpId);

            if (existingEmp != null)
            {
                existingEmp.EmpName = emp.EmpName;
                existingEmp.EmpTitle = emp.EmpTitle;
                existingEmp.Leave = emp.Leave; 
            }

            if (existingSal != null && salary != null)
            {
                existingSal.HoursWorked = salary.HoursWorked;
                existingSal.HourlyRate = salary.HourlyRate;
                existingSal.OverTimeHours = salary.OverTimeHours;
                existingSal.OverTimePay = salary.OverTimePay; 
                existingSal.NetPay = salary.NetPay;
            }
        }

        public void Delete(string empId)
        {
            _employees.RemoveAll(e => e.EmpId == empId);
            _salaries.RemoveAll(s => s.EmpId == empId);
        }
    }
}