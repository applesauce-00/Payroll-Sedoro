<<<<<<< HEAD
﻿using PayrollData;
using PayrollService;
using PayrollDataLayer;
using PayrollDataService;
=======
﻿using System;
namespace payroll
>>>>>>> parent of a3b31bb (add additional name)

namespace Payroll_Sedoro
{
    internal class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            EmployeeInfos info = new EmployeeInfos();
            PayrollServices payroll = new PayrollServices();
            Employee emp = info.GetEmployee();
=======
            string empid = "m";
            int hourlyRate = 600, hoursWorked = 80;


            int overtimeHours = 3;
            double overtimeIncrease = 1.25;

            double leave = 1;
            double pagibigBiweekly = 200 / 2;
>>>>>>> parent of a3b31bb (add additional name)

            short attempt = 0;
            bool valid = false;

<<<<<<< HEAD
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
=======
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
>>>>>>> parent of a3b31bb (add additional name)
            }
        }

<<<<<<< HEAD
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
=======
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
>>>>>>> parent of a3b31bb (add additional name)
            Console.WriteLine($"Gross Basic Pay: {gross}");
            Console.WriteLine($"Overtime ({emp.OvertimeHours} hr/s): {overtime}");
            Console.WriteLine($"Leave Deduction ({emp.LeaveDays} day/s): {leaveDeduction}");
            Console.WriteLine($"Total Gross Pay: {totalGross}");
<<<<<<< HEAD

            Console.WriteLine("\nTAXES");
            Console.WriteLine($"PAG-IBIG: {tax.Pagibig}");
            Console.WriteLine($"SSS: {tax.SSS}");
            Console.WriteLine($"Philhealth: {tax.Philhealth}");
            Console.WriteLine($"Total Tax Deduction: {payroll.TotalTax()}");

            Console.WriteLine($"\nNETPAY: {netPay}");
        }
    }
}
=======
            Console.WriteLine("");
            Console.WriteLine($"PAG-IBIG: {pagibigBiweekly}");
            //Console.WriteLine($"SSS: {}");
            //Console.WriteLine($"PhilHealth: {pagibigBiweekly}");
            Console.WriteLine("");
            Console.WriteLine($"NETPAY: {netPay}");
        }
    }
}
>>>>>>> parent of a3b31bb (add additional name)
