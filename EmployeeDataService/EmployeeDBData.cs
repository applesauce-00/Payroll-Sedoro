using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace EmployeeDataService
{
    public class EmployeeDBData : IEmployeeDataService
    {
        // Connection string
        private string connectionString =
            "Data Source=PIPS\\SQLEXPRESS;Initial Catalog=Payroll_Employee;Integrated Security=True;TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public EmployeeDBData()
        {
            sqlConnection = new SqlConnection(connectionString);
            AddSeeds(); 
        }

        
        private void AddSeeds()                                    //dummy seeds
        {
            var existing = GetEmployees();
            if (existing.Count == 0)
            {
                Employee emp = new Employee
                {
                    EmpId = "kirby",
                    EmpName = "Kirby T. Sedoro",
                    EmpTitle = "Operation Manager",
                    HourlyRate = 300,
                    HoursWorked = 80,
                    OverTime = 3,
                    LeaveDays = 1,
                    NetPay = 0
                };

                Add(emp);
            }
        }

        public void Add(Employee emp)   
        {
            string insertStatement = @"
                INSERT INTO Employee
                (empId, empName, empTitle, hourlyRate, hoursWorked, overTime, leaveDays, netPay)
                VALUES
                (@EmpId, @EmpName, @EmpTitle, @HourlyRate, @HoursWorked, @OverTime, @LeaveDays, @NetPay)";

            using SqlCommand insertCommand = new SqlCommand(insertStatement, sqlConnection);

            insertCommand.Parameters.AddWithValue("@EmpId", emp.EmpId);
            insertCommand.Parameters.AddWithValue("@EmpName", (object?)emp.EmpName ?? DBNull.Value);
            insertCommand.Parameters.AddWithValue("@EmpTitle", (object?)emp.EmpTitle ?? DBNull.Value);
            insertCommand.Parameters.AddWithValue("@HourlyRate", emp.HourlyRate);
            insertCommand.Parameters.AddWithValue("@HoursWorked", emp.HoursWorked);
            insertCommand.Parameters.AddWithValue("@OverTime", (object?)emp.OverTime ?? DBNull.Value);
            insertCommand.Parameters.AddWithValue("@LeaveDays", (object?)emp.LeaveDays ?? DBNull.Value);
            insertCommand.Parameters.AddWithValue("@NetPay", (object?)emp.NetPay ?? DBNull.Value);

            sqlConnection.Open();
            insertCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Employee> GetEmployees()
        {
            string selectStatement = "SELECT * FROM Employee";
            using SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);

            sqlConnection.Open();
            using SqlDataReader reader = selectCommand.ExecuteReader();

            var employees = new List<Employee>();

            while (reader.Read())
            {
                Employee emp = new Employee
                {
                    EmpId = reader["empId"].ToString(),
                    EmpName = reader["empName"]?.ToString(),
                    EmpTitle = reader["empTitle"]?.ToString(),
                    HourlyRate = Convert.ToInt32(reader["hourlyRate"]),
                    HoursWorked = Convert.ToInt32(reader["hoursWorked"]),
                    OverTime = reader["overTime"] == DBNull.Value ? 0 : Convert.ToInt32(reader["overTime"]),
                    LeaveDays = reader["leaveDays"] == DBNull.Value ? 0 : Convert.ToInt32(reader["leaveDays"]),
                    NetPay = reader["netPay"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["netPay"])
                };

                employees.Add(emp);
            }

            sqlConnection.Close();
            return employees;
        }

        public Employee? GetById(string empId)
        {
            string selectStatement = "SELECT * FROM Employee WHERE empId = @EmpId";
            using SqlCommand selectCommand = new SqlCommand(selectStatement, sqlConnection);
            selectCommand.Parameters.AddWithValue("@EmpId", empId);

            sqlConnection.Open();
            using SqlDataReader reader = selectCommand.ExecuteReader();

            Employee? emp = null;

            if (reader.Read())
            {
                emp = new Employee
                {
                    EmpId = reader["empId"].ToString(),
                    EmpName = reader["empName"]?.ToString(),
                    EmpTitle = reader["empTitle"]?.ToString(),
                    HourlyRate = Convert.ToInt32(reader["hourlyRate"]),
                    HoursWorked = Convert.ToInt32(reader["hoursWorked"]),
                    OverTime = reader["overTime"] == DBNull.Value ? 0 : Convert.ToInt32(reader["overTime"]),
                    LeaveDays = reader["leaveDays"] == DBNull.Value ? 0 : Convert.ToInt32(reader["leaveDays"]),
                    NetPay = reader["netPay"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["netPay"])
                };
            }

            sqlConnection.Close();
            return emp;
        }

        public void Update(Employee emp)
        {
            string updateStatement = @"
                UPDATE Employee SET
                    empName = @EmpName,
                    empTitle = @EmpTitle,
                    hourlyRate = @HourlyRate,
                    hoursWorked = @HoursWorked,
                    overTime = @OverTime,
                    leaveDays = @LeaveDays,
                    netPay = @NetPay
                WHERE empId = @EmpId";

            using SqlCommand updateCommand = new SqlCommand(updateStatement, sqlConnection);

            updateCommand.Parameters.AddWithValue("@EmpId", emp.EmpId);
            updateCommand.Parameters.AddWithValue("@EmpName", (object?)emp.EmpName ?? DBNull.Value);
            updateCommand.Parameters.AddWithValue("@EmpTitle", (object?)emp.EmpTitle ?? DBNull.Value);
            updateCommand.Parameters.AddWithValue("@HourlyRate", emp.HourlyRate);
            updateCommand.Parameters.AddWithValue("@HoursWorked", emp.HoursWorked);
            updateCommand.Parameters.AddWithValue("@OverTime", (object?)emp.OverTime ?? DBNull.Value);
            updateCommand.Parameters.AddWithValue("@LeaveDays", (object?)emp.LeaveDays ?? DBNull.Value);
            updateCommand.Parameters.AddWithValue("@NetPay", (object?)emp.NetPay ?? DBNull.Value);

            sqlConnection.Open();
            updateCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void Delete(string empId)
        {
            string deleteStatement = "DELETE FROM Employee WHERE empId = @EmpId";

            using SqlCommand deleteCommand = new SqlCommand(deleteStatement, sqlConnection);
            deleteCommand.Parameters.AddWithValue("@EmpId", empId);

            sqlConnection.Open();
            deleteCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
