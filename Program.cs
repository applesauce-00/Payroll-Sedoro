using System;
namespace payroll

{
    internal class Program
    {
        static void Main(string[] args)
        {
            string empid = "m";
            int hourlyRate = 600, hoursWorked = 80;


            int overtimeHours = 3;
            double overtimeIncrease = 1.25;

            double leave = 1;
            double pagibigBiweekly = 200 / 2;

            Console.Write("Enter Employee ID: ");
            string userEmpid = Console.ReadLine();

            if (isValidEmployee(userEmpid, empid))
            {
                double gross = grossComputation(hourlyRate, hoursWorked);
                double overtimeTotal = overtimeComputation(hourlyRate, overtimeHours, overtimeIncrease);
                double leaveDeduction = leaveDeductionComputation(hourlyRate, leave);
                double totalGross = totalGrossComputation(gross, overtimeTotal, leaveDeduction);
                double netPay = netPayComputation(totalGross, pagibigBiweekly);
                displayPayroll(userEmpid, hourlyRate, hoursWorked, gross, overtimeHours, overtimeTotal, leaveDeduction, totalGross, pagibigBiweekly, netPay);
            }
            else
            {
                Console.WriteLine("Incorrect Employee ID");
            }

        }

        static bool isValidEmployee(string inputId, string validId)
        {
            return inputId == validId;
        }

        static double overtimeComputation(int rate, double hours, double increase)
        {
            return rate * (hours * increase);
        }
        static double leaveDeductionComputation(int rate, double leave)
        {
            return rate * leave;
        }
        static double grossComputation(int rate, int hours)
        {
            return rate * hours;
        }
        static double totalGrossComputation(double gross, double overtimeTotal, double leaveDeduction)
        {
            double totalGross = (gross + overtimeTotal) - leaveDeduction;
            return totalGross;
        }
        static double netPayComputation(double totalGross, double pagibig)
        {
            return totalGross - pagibig;
        }
        static void displayPayroll(string empid, int hourlyRate, int hoursWorked, double gross, int overtimeHours, double overtimeTotal, double leaveDeduction, double totalGross, double pagibigBiweekly, double netPay)
        {
            Console.WriteLine("Employee Management System");
            Console.WriteLine("PAYROLL OF BIWEEKLY");
            Console.Write($"Employee ID: {empid}\n");
            Console.WriteLine($"Hourly Rate: {hourlyRate}");
            Console.WriteLine($"Hours Worked: {hoursWorked}");
            Console.WriteLine($"Gross Basic Pay: {gross}");
            Console.WriteLine($"Overtime ({overtimeHours} hr/s,): {overtimeTotal}");
            Console.WriteLine($"Leave Deduction: {leaveDeduction}");
            Console.WriteLine($"Total Gross Pay: {totalGross}");
            Console.WriteLine("");
            Console.WriteLine($"PAG-IBIG: {pagibigBiweekly}");
            //Console.WriteLine($"SSS: {}");
            //Console.WriteLine($"PhilHealth: {pagibigBiweekly}");
            Console.WriteLine("");
            Console.WriteLine($"NETPAY: {netPay}");
        }
    }
}