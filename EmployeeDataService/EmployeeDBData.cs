using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace EmployeeDataService
{
    public class EmployeeDBData : IEmployeeDataService
    {
        private string connectionString = "Data Source=PIPS\\SQLEXPRESS;Initial Catalog=PayrollDatabase;Integrated Security=True;TrustServerCertificate=True;";


        public Employee GetById(string empId)
        {
            string query = @"
        SELECT e.EmpId, e.EmpName, e.EmpTitle, e.Leave, 
               s.HoursWorked, s.HourlyRate, s.OverTimeHours, s.OverTimePay, s.NetPay 
        FROM Employee e
        LEFT JOIN Salary s ON e.EmpId = s.EmpId
        WHERE e.EmpId = @EmpId";

            using SqlConnection conn = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmpId", empId);

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var emp = new Employee
                {
                    EmpId = reader["EmpId"].ToString(),
                    EmpName = reader["EmpName"].ToString(),
                    EmpTitle = reader["EmpTitle"].ToString(),
                    Leave = Convert.ToInt32(reader["Leave"])
                };

                if (reader["HoursWorked"] != DBNull.Value)
                {
                    emp.SalaryInfo = new Salary
                    {
                        EmpId = emp.EmpId,
                        HoursWorked = (decimal)reader["HoursWorked"],
                        HourlyRate = (decimal)reader["HourlyRate"],
                        OverTimeHours = (decimal)reader["OverTimeHours"],
                        OverTimePay = (decimal)reader["OverTimePay"],
                        NetPay = (decimal)reader["NetPay"]
                    };
                }

                return emp;
            }
            return null;
        }

        public void Update(Employee emp, Salary salary)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                string updateEmp = "UPDATE Employee SET EmpName=@Name, EmpTitle=@Title, Leave=@Leave WHERE EmpId=@Id";
                using SqlCommand cmd1 = new SqlCommand(updateEmp, conn, transaction);
                cmd1.Parameters.AddWithValue("@Name", emp.EmpName);
                cmd1.Parameters.AddWithValue("@Title", emp.EmpTitle);
                cmd1.Parameters.AddWithValue("@Leave", emp.Leave);
                cmd1.Parameters.AddWithValue("@Id", emp.EmpId);
                cmd1.ExecuteNonQuery();

                string updateSalary = "UPDATE Salary SET HoursWorked=@HW, HourlyRate=@Rate, OverTimeHours=@OT, NetPay=@NP, OverTimePay=@OTP WHERE EmpId=@Id";
                using SqlCommand cmd2 = new SqlCommand(updateSalary, conn, transaction);
                cmd2.Parameters.AddWithValue("@HW", salary.HoursWorked);
                cmd2.Parameters.AddWithValue("@Rate", salary.HourlyRate);
                cmd2.Parameters.AddWithValue("@OT", salary.OverTimeHours);
                cmd2.Parameters.AddWithValue("@NP", salary.NetPay);
                cmd2.Parameters.AddWithValue("@OTP", salary.OverTimePay);
                cmd2.Parameters.AddWithValue("@Id", emp.EmpId); 
                cmd2.ExecuteNonQuery();

                transaction.Commit();
            }
            catch { transaction.Rollback(); throw; }
        }

        public void Add(Employee emp, Salary salary)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                string sqlEmp = "INSERT INTO Employee (EmpId, EmpName, EmpTitle, Leave) VALUES (@Id, @Name, @Title, @Leave)";
                using SqlCommand cmd1 = new SqlCommand(sqlEmp, conn, transaction);
                cmd1.Parameters.AddWithValue("@Id", emp.EmpId);
                cmd1.Parameters.AddWithValue("@Name", emp.EmpName);
                cmd1.Parameters.AddWithValue("@Title", emp.EmpTitle);
                cmd1.Parameters.AddWithValue("@Leave", emp.Leave);
                cmd1.ExecuteNonQuery();

                string sqlSal = "INSERT INTO Salary (EmpId, HoursWorked, HourlyRate, OverTimeHours, NetPay, OverTimePay) VALUES (@Id, @HW, @Rate, @OT, @NP, @OTP)";
                using SqlCommand cmd2 = new SqlCommand(sqlSal, conn, transaction);
                cmd2.Parameters.AddWithValue("@Id", salary.EmpId);
                cmd2.Parameters.AddWithValue("@HW", salary.HoursWorked);
                cmd2.Parameters.AddWithValue("@Rate", salary.HourlyRate);
                cmd2.Parameters.AddWithValue("@OT", salary.OverTimeHours);
                cmd2.Parameters.AddWithValue("@OTP", salary.OverTimePay);
                cmd2.Parameters.AddWithValue("@NP", salary.NetPay);
                cmd2.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(string empId)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using SqlTransaction transaction = conn.BeginTransaction();

            try
            {
                
                string delSal = "DELETE FROM Salary WHERE empId = @Id";
                using SqlCommand cmd1 = new SqlCommand(delSal, conn, transaction);
                cmd1.Parameters.AddWithValue("@Id", empId);
                cmd1.ExecuteNonQuery();

                string delEmp = "DELETE FROM Employee WHERE empId = @Id";
                using SqlCommand cmd2 = new SqlCommand(delEmp, conn, transaction);
                cmd2.Parameters.AddWithValue("@Id", empId);
                cmd2.ExecuteNonQuery();

                transaction.Commit();
            }
            catch { transaction.Rollback(); throw; }
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            string query = @"
        SELECT e.EmpId, e.EmpName, e.EmpTitle, 
               s.HoursWorked, s.HourlyRate, s.OverTimeHours, s.OverTimePay, s.NetPay
        FROM Employee e
        LEFT JOIN Salary s ON e.EmpId = s.EmpId";

            using var conn = new SqlConnection(connectionString);
            using var cmd = new SqlCommand(query, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var emp = new Employee
                {
                    EmpId = reader.GetString(reader.GetOrdinal("EmpId")),
                    EmpName = reader.GetString(reader.GetOrdinal("EmpName")),
                    EmpTitle = reader.GetString(reader.GetOrdinal("EmpTitle"))
                };

                int netPayOrd = reader.GetOrdinal("NetPay");
                if (!reader.IsDBNull(netPayOrd))
                {
                    emp.SalaryInfo = new Salary
                    {
                        EmpId = emp.EmpId,
                        HoursWorked = reader.GetDecimal(reader.GetOrdinal("HoursWorked")),
                        HourlyRate = reader.GetDecimal(reader.GetOrdinal("HourlyRate")),
                        OverTimeHours = reader.GetDecimal(reader.GetOrdinal("OverTimeHours")),
                        OverTimePay = reader.GetDecimal(reader.GetOrdinal("OverTimePay")),
                        NetPay = reader.GetDecimal(netPayOrd)
                    };
                }
                list.Add(emp);
            }
            return list;
        }
    }
}