using EmployeeDataStorage;
using PayrollService;
using EmployeeDataService;
using PayrollDisplay;  
using System;

namespace Payroll_Sedoro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeInfo repo = new EmployeeInfo();
            PayrollComputation payroll = new PayrollComputation();
            PayrollShow display = new PayrollShow(); 

            Employee emp = repo.GetEmployee();

            short attempt = 0;
            bool valid = false;
            while (attempt < 3 && !valid)
            {
                Console.Write("Enter Employee ID: ");
                string userEmpid = Console.ReadLine();

                if (payroll.IsValidEmployee(userEmpid, emp.EmpId))
                {
                    double gross = payroll.GrossComputation(emp.HourlyRate, emp.HoursWorked);
                    double overtime = payroll.OvertimeComputation(emp.HourlyRate, emp.OvertimeHours);
                    double leave = payroll.LeaveDeduction(emp.HourlyRate, emp.LeaveDays);

                    double totalGross = payroll.TotalGross(gross, overtime, leave);
                    double netPay = payroll.NetPay(totalGross);

                    display.ShowPayroll(emp, gross, overtime, leave, totalGross, netPay, payroll);
                    valid = true;
                }
                else
                {
                    attempt++;
                    Console.WriteLine("Incorrect Employee ID\n");

                    if (attempt == 3)
                    {
                        Console.WriteLine("Maximum attempts reached. Exiting program...");
                        return;
                    }
                }
            }
        }
    }
}
