using EmployeeDataService;
using EmployeeDataStorage;
using System;
using System.Collections.Generic;

namespace EmployeeDataStorage
{
    public class EmployeeRepository
    {
        private List<Employee> employees = new List<Employee>();
        private List<PayrollRecord> payrolls = new List<PayrollRecord>();

        public EmployeeRepository()
        {
            employees.Add(new Employee
            {
                EmpId = "kirby",
                Name = "Kirby T. Sedoro",
                Title = "Operation Manager",
                HourlyRate = 300
            });
        }

        public void AddEmployee(Employee emp)
        {
            employees.Add(emp);
        }

        public void UpdateEmployee(Employee emp)
        {
            Employee existing = GetEmployeeById(emp.EmpId);
            if (existing != null)
            {
                existing.Name = emp.Name;
                existing.Title = emp.Title;
                existing.HourlyRate = emp.HourlyRate;
                existing.HoursWorked = emp.HoursWorked;
                existing.OvertimeHours = emp.OvertimeHours;
                existing.LeaveDays = emp.LeaveDays;
            }
        }

        public void DeleteEmployee(string empId)
        {
            List<Employee> toRemove = new List<Employee>();
            foreach (Employee e in employees)
            {
                if (e.EmpId == empId)
                {
                    toRemove.Add(e);
                }
            }

            foreach (Employee e in toRemove)
            {
                employees.Remove(e);
            }
        }

        public Employee GetEmployeeById(string empId)
        {
            foreach (Employee e in employees)
            {
                if (e.EmpId == empId)
                {
                    return e;
                }
            }
            return null;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> all = new List<Employee>();
            foreach (Employee e in employees)
            {
                all.Add(e);
            }
            return all;
        }


        public void SavePayroll(Employee emp, double gross, double overtime, double leave, double totalGross, double netPay)
        {
            payrolls.Add(new PayrollRecord
            {
                EmployeeId = emp.EmpId,
                PayDate = DateTime.Now,
                GrossPay = gross,
                OvertimePay = overtime,
                LeaveDeduction = leave,
                TotalGross = totalGross,
                NetPay = netPay
            });
        }

        public List<PayrollRecord> GetPayrollHistory(string empId)
        {
            List<PayrollRecord> result = new List<PayrollRecord>();
            foreach (PayrollRecord p in payrolls)
            {
                if (p.EmployeeId == empId)
                {
                    result.Add(p);
                }
            }
            return result;
        }


        public class PayrollRecord
        {
            public string EmployeeId { get; set; }
            public DateTime PayDate { get; set; }
            public double GrossPay { get; set; }
            public double OvertimePay { get; set; }
            public double LeaveDeduction { get; set; }
            public double TotalGross { get; set; }
            public double NetPay { get; set; }
        }
    }
}
