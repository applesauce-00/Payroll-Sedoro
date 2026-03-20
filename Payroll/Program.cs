using EmployeeDataService;
using EmployeeDataStorage;
using PayrollService;
using System;
using System.Collections.Generic;

namespace Payroll_Sedoro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository empRepo = new EmployeeRepository();
            PayrollComputation payroll = new PayrollComputation();

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

        static void AdminMenu(EmployeeRepository empRepo, PayrollComputation payroll)
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
                        AddEmployee(empRepo);
                        break;
                    case "2":
                        EditEmployee(empRepo);
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

        static void EmployeeLogin(EmployeeRepository empRepo, PayrollComputation payroll)
        {
            Console.Write("Enter your Employee ID: ");
            string empId = Console.ReadLine()?.Trim();
            Employee emp = empRepo.GetEmployeeById(empId);

            if (emp == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            PayrollResult result = payroll.ComputePayroll(emp);
            ShowPayroll(emp, result, payroll);

            Console.WriteLine("\n--- Payroll History ---");
            var history = empRepo.GetPayrollHistory(emp.EmpId);
            foreach (var record in history)
                Console.WriteLine($"{record.PayDate}: NetPay = {record.NetPay}");
        }

        static void AddEmployee(EmployeeRepository empRepo)
        {
            Employee newEmp = new Employee();
            Console.Write("Employee ID: "); newEmp.EmpId = Console.ReadLine();
            Console.Write("Name: "); newEmp.Name = Console.ReadLine();
            Console.Write("Title: "); newEmp.Title = Console.ReadLine();
            Console.Write("Hourly Rate: "); newEmp.HourlyRate = Convert.ToInt32(Console.ReadLine());
            Console.Write("Hours Worked: "); newEmp.HoursWorked = Convert.ToInt32(Console.ReadLine());
            Console.Write("Overtime Hours: "); newEmp.OvertimeHours = Convert.ToInt32(Console.ReadLine());
            Console.Write("Leave Days: "); newEmp.LeaveDays = Convert.ToDouble(Console.ReadLine());

            empRepo.AddEmployee(newEmp);
            Console.WriteLine("Employee added.");
        }

        static void EditEmployee(EmployeeRepository empRepo)
        {
            Console.Write("Enter Employee ID to edit: ");
            string editId = Console.ReadLine();
            Employee editEmp = empRepo.GetEmployeeById(editId);

            if (editEmp != null)
            {
                Console.Write("New Name (leave blank to keep current): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    editEmp.Name = name;
                }

                Console.Write("New Title (leave blank to keep current): ");
                string title = Console.ReadLine();
                if (!string.IsNullOrEmpty(title))
                {
                    editEmp.Title = title;
                }

                Console.Write("New Hourly Rate (leave blank to keep current): ");
                string rateStr = Console.ReadLine();
                if (int.TryParse(rateStr, out int rate))
                {
                    editEmp.HourlyRate = rate;
                }

                Console.Write("New Hours Worked (leave blank to keep current): ");
                string hoursStr = Console.ReadLine();
                if (int.TryParse(hoursStr, out int hours))
                {
                    editEmp.HoursWorked = hours;
                }

                Console.Write("New Overtime Hours (leave blank to keep current): ");
                string otStr = Console.ReadLine();
                if (int.TryParse(otStr, out int ot))
                {
                    editEmp.OvertimeHours = ot;
                }

                Console.Write("New Leave Days (leave blank to keep current): ");
                string leaveStr = Console.ReadLine();
                if (double.TryParse(leaveStr, out double leave))
                {
                    editEmp.LeaveDays = leave;
                }

                empRepo.UpdateEmployee(editEmp);
                Console.WriteLine("Employee updated.");
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static void DeleteEmployee(EmployeeRepository empRepo)
        {
            Console.Write("Enter Employee ID to delete: ");
            string delId = Console.ReadLine();
            empRepo.DeleteEmployee(delId);
            Console.WriteLine("Employee deleted.");
        }

        static void ViewAllEmployees(EmployeeRepository empRepo, PayrollComputation payroll)
        {
            var allEmp = empRepo.GetAllEmployees();
            Console.WriteLine("\n--- Employees ---\n");
            Console.WriteLine("\nTotal Employee = " + allEmp.Count);

            foreach (var e in allEmp)
            {
                PayrollResult resultAll = payroll.ComputePayroll(e);
                Console.WriteLine("");
                Console.WriteLine($"ID: {e.EmpId}");
                Console.WriteLine($"Name: {e.Name}");
                Console.WriteLine($"Title: {e.Title}");
                Console.WriteLine($"Hourly Rate: {e.HourlyRate}");
                Console.WriteLine($"Hours Worked: {e.HoursWorked}");
                Console.WriteLine($"Overtime Hours: {e.OvertimeHours}");
                Console.WriteLine($"Leave Days: {e.LeaveDays}");
                Console.WriteLine($"NetPay: {resultAll.NetPay}");
                Console.WriteLine("---------------------------");
            }
        }

        static void SearchEmployee(EmployeeRepository empRepo, PayrollComputation payroll)
        {
            Console.Write("Enter Employee ID to search: ");
            string searchId = Console.ReadLine();
            Employee emp = empRepo.GetEmployeeById(searchId);

            if (emp != null)
            {
                ShowEmployee(emp);
            }
            else
            {
                Console.WriteLine("Employee not found.");
            }
        }

        static void ShowPayroll(Employee emp, PayrollResult result, PayrollComputation payroll)
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
            Console.WriteLine($"Gross Basic Pay: {result.Gross}");
            Console.WriteLine($"Overtime ({emp.OvertimeHours} hr/s): {result.Overtime}");
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
            Console.WriteLine($"Name: {emp.Name}");
            Console.WriteLine($"Title: {emp.Title}");
            Console.WriteLine($"Hourly Rate: {emp.HourlyRate}");
        }
    }
}
