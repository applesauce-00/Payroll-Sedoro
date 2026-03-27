using EmployeeDataService;
using PayrollService;
using System;
using System.Collections.Generic;

namespace Payroll_Sedoro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeService empRepo = new EmployeeService();
            PayrollComputation payroll = new PayrollComputation();

            var allEmployees = empRepo.GetEmployees();
            foreach (var emp in allEmployees)
            {
                var result = payroll.ComputePayroll(emp);
                emp.NetPay = (decimal)result.NetPay;
                empRepo.Update(emp);
            }

            Console.WriteLine("Select your role:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Employee");
            Console.Write("Choice: ");
            string role = Console.ReadLine()?.Trim();

            if (role == "1")
            {
                AdminMenu(empRepo, payroll);
            }
            else if (role == "2")
            {
                EmployeeLogin(empRepo, payroll);
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }
        }

        static void AdminMenu(EmployeeService empRepo, PayrollComputation payroll)
        {
            const string adminUser = "admin";
            const string adminPass = "admin123";

            Console.Write("Enter Admin Username: ");
            string user = Console.ReadLine();
            Console.Write("Enter Admin Password: ");
            string pass = Console.ReadLine();

            if (user != adminUser || pass != adminPass)
            {
                Console.WriteLine("Invalid credentials.");
                return;
            }

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Admin Menu ---");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Edit Employee");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. View All Employees");
                Console.WriteLine("5. Search Employee");
                Console.WriteLine("6. Exit");

                Console.Write("Choice: ");
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddEmployee(empRepo, payroll);
                        break;
                    case "2":
                        EditEmployee(empRepo, payroll);
                        break;
                    case "3":
                        DeleteEmployee(empRepo);
                        break;
                    case "4":
                        ViewAllEmployees(empRepo, payroll);
                        break;
                    case "5":
                        SearchEmployee(empRepo, payroll);
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void EmployeeLogin(EmployeeService empRepo, PayrollComputation payroll)
        {
            Console.Write("Enter your Employee ID: ");
            string empId = Console.ReadLine()?.Trim();
            Employee? emp = empRepo.GetById(empId);

            if (emp == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            PayrollResult result = payroll.ComputePayroll(emp);
            ShowPayroll(emp, result, payroll);
        }

        static void AddEmployee(EmployeeService empRepo, PayrollComputation payroll)
        {
            Employee newEmp = new Employee();
            Console.Write("Employee ID: "); newEmp.EmpId = Console.ReadLine();
            Console.Write("Name: "); newEmp.EmpName = Console.ReadLine();
            Console.Write("Title: "); newEmp.EmpTitle = Console.ReadLine();
            Console.Write("Hourly Rate: "); newEmp.HourlyRate = (int)Convert.ToDecimal(Console.ReadLine());
            Console.Write("Hours Worked: "); newEmp.HoursWorked = Convert.ToInt32(Console.ReadLine());
            Console.Write("Overtime Hours: "); newEmp.OverTime = Convert.ToInt32(Console.ReadLine());
            Console.Write("Leave Days: "); newEmp.LeaveDays = (int)Convert.ToDecimal(Console.ReadLine());

            PayrollResult result = payroll.ComputePayroll(newEmp);
            newEmp.NetPay = (decimal)result.NetPay;

            empRepo.Add(newEmp);
            Console.WriteLine("Employee added.");
        }

        static void EditEmployee(EmployeeService empRepo, PayrollComputation payroll)
        {
            Console.Write("Enter Employee ID to edit: ");
            string editId = Console.ReadLine();
            Employee? editEmp = empRepo.GetById(editId);

            if (editEmp != null)
            {
                Console.Write("New Name (leave blank to keep current): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name)) editEmp.EmpName = name;

                Console.Write("New Title (leave blank to keep current): ");
                string title = Console.ReadLine();
                if (!string.IsNullOrEmpty(title)) editEmp.EmpTitle = title;

                Console.Write("New Hourly Rate (leave blank to keep current): ");
                string rateStr = Console.ReadLine();
                if (decimal.TryParse(rateStr, out decimal rate)) editEmp.HourlyRate = (int)rate;

                Console.Write("New Hours Worked (leave blank to keep current): ");
                string hoursStr = Console.ReadLine();
                if (int.TryParse(hoursStr, out int hours)) editEmp.HoursWorked = hours;

                Console.Write("New Overtime Hours (leave blank to keep current): ");
                string otStr = Console.ReadLine();
                if (int.TryParse(otStr, out int ot)) editEmp.OverTime = ot;

                Console.Write("New Leave Days (leave blank to keep current): ");
                string leaveStr = Console.ReadLine();
                if (double.TryParse(leaveStr, out double leave)) editEmp.LeaveDays = (int)leave;

                PayrollResult result = payroll.ComputePayroll(editEmp);
                editEmp.NetPay = (decimal)result.NetPay;

                empRepo.Update(editEmp);
                Console.WriteLine("Employee updated with NetPay: " + editEmp.NetPay);
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static void DeleteEmployee(EmployeeService empRepo)
        {
            Console.Write("Enter Employee ID to delete: ");
            string delId = Console.ReadLine();
            empRepo.Delete(delId);
            Console.WriteLine("Employee deleted.");
        }

        static void ViewAllEmployees(EmployeeService empRepo, PayrollComputation payroll)
        {
            var allEmp = empRepo.GetEmployees();
            Console.WriteLine("\n--- Employees ---\n");
            Console.WriteLine("Total Employees = " + allEmp.Count);

            foreach (var e in allEmp)
            {
                PayrollResult resultAll = payroll.ComputePayroll(e);
                Console.WriteLine("");
                Console.WriteLine($"ID: {e.EmpId}");
                Console.WriteLine($"Name: {e.EmpName}");
                Console.WriteLine($"Title: {e.EmpTitle}");
                Console.WriteLine($"Hourly Rate: {e.HourlyRate}");
                Console.WriteLine($"Hours Worked: {e.HoursWorked}");
                Console.WriteLine($"Overtime Hours: {e.OverTime}");
                Console.WriteLine($"Leave Days: {e.LeaveDays}");
                Console.WriteLine($"NetPay: {resultAll.NetPay}");
                Console.WriteLine("---------------------------");
            }
        }

        static void SearchEmployee(EmployeeService empRepo, PayrollComputation payroll)
        {
            Console.Write("Enter Employee ID to search: ");
            string searchId = Console.ReadLine();
            Employee? emp = empRepo.GetById(searchId);

            if (emp != null)
                ShowEmployee(emp);
            else
                Console.WriteLine("Employee not found.");
        }

        static void ShowPayroll(Employee emp, PayrollResult result, PayrollComputation payroll)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("    Employee Management System");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("              PAYROLL");
            Console.WriteLine("------------------------------------");

            Console.WriteLine($"Employee ID: {emp.EmpId}");
            Console.WriteLine($"Employee Name: {emp.EmpName}");
            Console.WriteLine($"Employee Title: {emp.EmpTitle}");

            Console.WriteLine($"\nHourly Rate: {emp.HourlyRate}");
            Console.WriteLine($"Hours Worked: {emp.HoursWorked}");
            Console.WriteLine($"Gross Basic Pay: {result.Gross}");
            Console.WriteLine($"Overtime ({emp.OverTime} hr/s): {result.Overtime}");
            Console.WriteLine($"Leave Deduction ({emp.LeaveDays} day/s): {result.LeaveDeduction}");
            Console.WriteLine($"Total Gross Pay: {result.TotalGross}");

            Console.WriteLine("\nTAXES");
            Console.WriteLine($"PAG-IBIG: {payroll.Pagibig()}");
            Console.WriteLine($"SSS: {payroll.SSS()}");
            Console.WriteLine($"Philhealth: {payroll.Philhealth()}");
            Console.WriteLine($"Total Tax Deduction: {payroll.TotalTax()}");

            Console.WriteLine($"\nNETPAY: {result.NetPay}");
        }

        static void ShowEmployee(Employee emp)
        {
            Console.WriteLine("\n--- Employee Info ---");
            Console.WriteLine($"ID: {emp.EmpId}");
            Console.WriteLine($"Name: {emp.EmpName}");
            Console.WriteLine($"Title: {emp.EmpTitle}");
            Console.WriteLine($"Hourly Rate: {emp.HourlyRate}");
        }
    }
}
