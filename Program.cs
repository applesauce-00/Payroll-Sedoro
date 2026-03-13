using PayrollDataService;
using PayrollService;
using PayrollData;

namespace Payroll_Sedoro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeInfos info = new EmployeeInfos();
            PayrollServices payroll = new PayrollServices();
            Employee emp = info.GetEmployee();

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

                    DisplayPayroll(emp, gross, overtime, leave, totalGross, netPay, payroll);
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

        static void DisplayPayroll(Employee emp, double gross, double overtime,
            double leaveDeduction, double totalGross, double netPay, PayrollServices payroll)
        {
            var tax = payroll.GetTaxes();
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
            Console.WriteLine($"PAG-IBIG: {tax.Pagibig}");
            Console.WriteLine($"SSS: {tax.SSS}");
            Console.WriteLine($"Philhealth: {tax.Philhealth}");
            Console.WriteLine($"Total Tax Deduction: {payroll.TotalTax()}");

            Console.WriteLine($"\nNETPAY: {netPay}");
        }
    }
}
