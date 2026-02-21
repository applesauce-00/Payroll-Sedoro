using System;
namespace Payroll_Sedoro
    
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string empid = "kirby";
            int hourlyRate = 600, hoursWorked = 80;
            int gross = hoursWorked * hourlyRate;
            int overtimeHours = 5;
            double overtimeIncrease = 1.25; 
            double overtimeAmount = hourlyRate * overtimeIncrease;
            double overtimeTotal = overtimeHours * overtimeAmount;
            double leave = 1;
            double leaveDeduction = leave * hourlyRate;
            double totalGross = totalGrossComputation(gross, overtimeTotal, leaveDeduction);
            double pagibigBiweekly = 200 / 2;
            double netPay = totalGross - pagibigBiweekly;


            Console.WriteLine("Employee Management System");
            Console.WriteLine("PAYROLL OF BIWEEKLY");

            Console.Write("Enter Employee ID: ");
            string userEmpid = Console.ReadLine();

            if (userEmpid == empid)
            {
                Console.WriteLine($"Hourly Rate: {hourlyRate}");
                Console.WriteLine($"Hours Worked: {hoursWorked}");
                Console.WriteLine($"Gross Basic Pay: {gross}");
                Console.WriteLine($"Overtime ({overtimeHours} hrs.): {overtimeTotal}");
                Console.WriteLine($"Leave ({leave} day/s): {leaveDeduction}");
                Console.WriteLine($"Total Gross Pay: {totalGross}");
                Console.WriteLine("");

                Console.WriteLine($"PAG-IBIG Tax: {pagibigBiweekly}");
                Console.WriteLine("");
                Console.WriteLine($"NETPAY: {netPay}");
            }  else
            {
                Console.WriteLine("Invalid Employee ID");
            }

        }

        static double totalGrossComputation(double gross, double overtimeTotal, double leaveDeduction)
        {
            double totalGross = (gross + overtimeTotal) - leaveDeduction;
        
        }
    }
}
