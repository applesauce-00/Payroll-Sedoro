using System;
using PayrollService;
using EmployeeDataService;

namespace PayrollDisplay
{
    public class PayrollShow
    {
        public void ShowPayroll(Employee emp, double gross, double overtime,
            double leaveDeduction, double totalGross, double netPay, PayrollComputation payroll)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("    Employee Management System");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("              PAYROLL");
            Console.WriteLine("------------------------------------");

            Console.WriteLine($"Employee ID: {emp.EmpId}");
            Console.WriteLine($"Employee Name: {emp.Name}");
            Console.WriteLine($"Employee Title: {emp.Title}");

            Console.WriteLine($"\nHourly Rate: {emp.HourlyRate}");
            Console.WriteLine($"Hours Worked: {emp.HoursWorked}");
            Console.WriteLine($"Gross Basic Pay: {gross}");
            Console.WriteLine($"Overtime ({emp.OvertimeHours} hr/s): {overtime}");
            Console.WriteLine($"Leave Deduction ({emp.LeaveDays} day/s): {leaveDeduction}");
            Console.WriteLine($"Total Gross Pay: {totalGross}");

            Console.WriteLine("\nTAXES");
            Console.WriteLine($"PAG-IBIG: {payroll.Pagibig()}");
            Console.WriteLine($"SSS: {payroll.SSS()}");
            Console.WriteLine($"Philhealth: {payroll.Philhealth()}");
            Console.WriteLine($"Total Tax Deduction: {payroll.TotalTax()}");

            Console.WriteLine($"\nNETPAY: {netPay}");
        }
    }
}
