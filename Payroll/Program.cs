using EmployeeDataService;
using PayrollService;
using System;

namespace Payroll_Sedoro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EmployeeService empRepo = new EmployeeService(new EmployeeDBData());
            PayrollComputation payroll = new PayrollComputation();

            UpdateAllEmployeeNetPays(empRepo, payroll);

            ShowMainMenu(empRepo, payroll);
        }

        static void UpdateAllEmployeeNetPays(EmployeeService empRepo, PayrollComputation payroll)
        {
            var employees = empRepo.GetEmployees();

            foreach (var emp in employees)
            {
                emp.SalaryInfo.NetPay = (decimal)payroll.ComputePayroll(emp, emp.SalaryInfo).NetPay;
                empRepo.Update(emp, emp.SalaryInfo);
            }
        }

        static void ShowMainMenu(EmployeeService empRepo, PayrollComputation payroll)
        {
            Console.WriteLine("Select your role:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Employee");
            Console.Write("Choice: ");

            string role = Console.ReadLine()?.Trim();

            if (role == "1")
                AdminMenu(empRepo, payroll);

            else if (role == "2")
                EmployeeLogin(empRepo, payroll);

            else
                Console.WriteLine("Invalid choice.");
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
                string choice = Console.ReadLine();

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
                        ViewAllEmployees(empRepo);
                        break;

                    case "5":
                        SearchEmployee(empRepo);
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
            Console.Write("Enter Employee ID: ");

            string empId = Console.ReadLine();

            Employee emp = empRepo.GetById(empId);

            if (emp == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            PayrollResult result = payroll.ComputePayroll(emp, emp.SalaryInfo);

            ShowPayroll(emp, emp.SalaryInfo, result, payroll);
        }

        static void AddEmployee(EmployeeService empRepo, PayrollComputation payroll)
        {
            try
            {
                Employee emp = new Employee();
                Salary sal = new Salary();

                Console.Write("ID: ");
                string id = Console.ReadLine();
                emp.EmpId = id;
                sal.EmpId = id; // Foreign Key

                Console.Write("Name: ");
                emp.EmpName = Console.ReadLine();

                Console.Write("Title: ");
                emp.EmpTitle = Console.ReadLine();

                Console.Write("Hourly Rate: ");
                sal.HourlyRate = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Hours Worked: ");
                sal.HoursWorked = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Overtime Hours: ");
                sal.OverTimeHours = Convert.ToDecimal(Console.ReadLine());

                Console.Write("Leave Day/s: ");
                emp.Leave = Convert.ToInt32(Console.ReadLine());

                // Calculation
                PayrollResult result = payroll.ComputePayroll(emp, sal);
                sal.NetPay = (decimal)result.NetPay;
                sal.OverTimePay = (decimal)result.Overtime;

                empRepo.Add(emp, sal);

                Console.WriteLine("Employee Added.");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        static void EditEmployee(EmployeeService empRepo, PayrollComputation payroll)
        {
            Console.Write("Enter Employee ID to edit: ");
            string editId = Console.ReadLine();
            // Get the objects
            Employee? editEmp = empRepo.GetById(editId);

            if (editEmp != null && editEmp.SalaryInfo != null)
            {
                
                Console.Write("New Name (leave blank to keep current): ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name)) editEmp.EmpName = name;

                Console.Write("New Title (leave blank to keep current): ");
                string title = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(title)) editEmp.EmpTitle = title;

                Console.Write("New Hourly Rate (leave blank to keep current): ");
                string rateStr = Console.ReadLine();
                if (decimal.TryParse(rateStr, out decimal rate)) editEmp.SalaryInfo.HourlyRate = rate;

                Console.Write("New Hours Worked (leave blank to keep current): ");
                string hoursStr = Console.ReadLine();
                if (decimal.TryParse(hoursStr, out decimal hours)) editEmp.SalaryInfo.HoursWorked = hours;

                Console.Write("New Overtime Hours (leave blank to keep current): ");
                string otStr = Console.ReadLine();
                if (decimal.TryParse(otStr, out decimal ot)) editEmp.SalaryInfo.OverTimeHours = ot;

                Console.Write("New Leave Day/s (leave blank to keep current): ");
                string leaveStr = Console.ReadLine();
                if (int.TryParse(leaveStr, out int leave)) editEmp.Leave = leave;

                // Re-calculate
                PayrollResult result = payroll.ComputePayroll(editEmp, editEmp.SalaryInfo);
                editEmp.SalaryInfo.NetPay = (decimal)result.NetPay;

                // Pass both objects to the service
                empRepo.Update(editEmp, editEmp.SalaryInfo);
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Employee record not found.");
            }
        }

        static void DeleteEmployee(EmployeeService empRepo)
        {
            Console.Write("Enter Employee ID: ");

            string id = Console.ReadLine();

            empRepo.Delete(id);

            Console.WriteLine("Deleted.");
        }

        static void ViewAllEmployees(EmployeeService empRepo)
        {
            var employees = empRepo.GetEmployees();

            foreach (var emp in employees)
            {
                ShowEmployee(emp);
            }
        }

        static void SearchEmployee(EmployeeService empRepo)
        {
            Console.Write("Enter ID: ");

            string id = Console.ReadLine();

            Employee emp = empRepo.GetById(id);

            if (emp != null)
                ShowEmployee(emp);

            else
                Console.WriteLine("Not found.");
        }

        static void ShowPayroll(Employee emp, Salary sal, PayrollResult result, PayrollComputation payroll)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("    Employee Management System");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("              PAYROLL");
            Console.WriteLine("------------------------------------");

            Console.WriteLine($"Employee ID: {emp.EmpId}");
            Console.WriteLine($"Employee Name: {emp.EmpName}");
            Console.WriteLine($"Employee Title: {emp.EmpTitle}");

            Console.WriteLine($"\nHourly Rate: {sal.HourlyRate}");
            Console.WriteLine($"Hours Worked: {sal.HoursWorked}");
            Console.WriteLine($"Gross Basic Pay: {result.Gross}");
            Console.WriteLine($"Overtime ({sal.OverTimeHours} hr/s): {sal.OverTimePay}");
            Console.WriteLine($"Leave: {emp.Leave} day/s");
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
            Console.WriteLine("-------------------");
            Console.WriteLine($"ID: {emp.EmpId}");
            Console.WriteLine($"Name: {emp.EmpName}");
            Console.WriteLine($"Title: {emp.EmpTitle}");
            Console.WriteLine($"NetPay: {emp.SalaryInfo.NetPay}");
        }
    }
}
