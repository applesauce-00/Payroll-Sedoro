using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
                employees.Add(new Employee
                {
                    EmpId = "kirby",
                    EmpName = "Kirby T. Sedoro",
                    EmpTitle = "Operation Manager",
                    HourlyRate = 300,
                    HoursWorked = 80,
                    OverTime = 3,
                    LeaveDays = 1,
                    NetPay = 0
                });

                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            using (var outputStream = File.Create(_jsonFileName))
            {
                JsonSerializer.Serialize<List<Employee>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true }),
                    employees);
            }
        }

        private void RetrieveDataFromJsonFile()
        {
            if (!File.Exists(_jsonFileName))
            {
                employees = new List<Employee>();
                SaveDataToJsonFile();
                return;
            }

            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                string json = jsonFileReader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(json))
                {
                    employees = new List<Employee>();
                }
                else
                {
                    employees = JsonSerializer.Deserialize<List<Employee>>(
                        json,
                        new JsonSerializerOptions
                        { PropertyNameCaseInsensitive = true }
                    ) ?? new List<Employee>();
                }
            }
        }

        public void Add(Employee emp)
        {
            RetrieveDataFromJsonFile();
            employees.Add(emp);
            SaveDataToJsonFile();
        }

        public List<Employee> GetEmployees()
        {
            RetrieveDataFromJsonFile();
            return employees;
        }

        public Employee? GetById(string empId)
        {
            RetrieveDataFromJsonFile();
            return employees.Where(x => x.EmpId == empId).FirstOrDefault();
        }

        public void Update(Employee emp)
        {
            RetrieveDataFromJsonFile();

            var existingEmployee = employees.FirstOrDefault(x => x.EmpId == emp.EmpId);

            if (existingEmployee != null)
            {
                existingEmployee.EmpName = emp.EmpName;
                existingEmployee.EmpTitle = emp.EmpTitle;
                existingEmployee.HourlyRate = emp.HourlyRate;
                existingEmployee.HoursWorked = emp.HoursWorked;
                existingEmployee.OverTime = emp.OverTime;
                existingEmployee.LeaveDays = emp.LeaveDays;
                existingEmployee.NetPay = emp.NetPay;
            }

            SaveDataToJsonFile();
        }

        public void Delete(string empId)
        {
            RetrieveDataFromJsonFile();

            var existingEmployee = employees.FirstOrDefault(x => x.EmpId == empId);

            if (existingEmployee != null)
            {
                employees.Remove(existingEmployee);
            }

            SaveDataToJsonFile();
        }
    }
}
