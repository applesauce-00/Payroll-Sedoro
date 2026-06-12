using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace EmployeeDataService
{
    public class EmployeeJsonData : IEmployeeDataService
    {
        private List<Employee> employees = new List<Employee>();
        private string _jsonFileName;

        public EmployeeJsonData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/employees.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();

            if (employees.Count <= 0)
            {
                // Seeds
                employees.Add(new Employee
                {
                    EmpId = "kirby",
                    EmpName = "Kirby T. Sedoro",
                    EmpTitle = "Operation Manager",
                    Leave = 0,
                    SalaryInfo = new Salary
                    {
                        EmpId = "kirby",
                        HourlyRate = 300,
                        HoursWorked = 80,
                        OverTimeHours = 3,
                        OverTimePay = 900,
                        Tax = 0,
                        NetPay = 24000
                    }
                });
                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(employees, options);
            File.WriteAllText(_jsonFileName, jsonString);
        }

        private void RetrieveDataFromJsonFile()
        {
            if (!File.Exists(_jsonFileName))
            {
                employees = new List<Employee>();
                return;
            }

            string json = File.ReadAllText(_jsonFileName);
            if (!string.IsNullOrWhiteSpace(json))
            {
                employees = JsonSerializer.Deserialize<List<Employee>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Employee>();
            }
        }

        public void Add(Employee emp, Salary salary)
        {
            RetrieveDataFromJsonFile();
            emp.SalaryInfo = salary;
            employees.Add(emp);
            SaveDataToJsonFile();
        }

        public List<Employee> GetEmployees()
        {
            RetrieveDataFromJsonFile();
            return employees;
        }

        public Employee GetById(string empId)
        {
            RetrieveDataFromJsonFile();
            return employees.FirstOrDefault(x => x.EmpId == empId);
        }

        public void Update(Employee emp, Salary salary)
        {
            RetrieveDataFromJsonFile();
            var existing = employees.FirstOrDefault(x => x.EmpId == emp.EmpId);

            if (existing != null)
            {
                existing.EmpName = emp.EmpName;
                existing.EmpTitle = emp.EmpTitle;
                existing.Leave = emp.Leave;
                existing.SalaryInfo = salary;
            }
            SaveDataToJsonFile();
        }

        public void Delete(string empId)
        {
            RetrieveDataFromJsonFile();
            employees.RemoveAll(x => x.EmpId == empId);
            SaveDataToJsonFile();
        }
    }
}