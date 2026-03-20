using EmployeeDataService;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace EmployeeDataStorage
{
    public class EmployeeRepository
    {
        private readonly EmployeeDBData db = new EmployeeDBData();

        public void AddEmployee(Employee emp)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Employee
                (empId, empName, empTitle, hourlyRate, hoursWorked, overTime, leaveDays, netPay)
                VALUES (@Id, @Name, @Title, @Rate, @Hours, @OT, @Leave, @NetPay)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", emp.EmpId);
                cmd.Parameters.AddWithValue("@Name", emp.EmpName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Title", emp.EmpTitle ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Rate", emp.HourlyRate);
                cmd.Parameters.AddWithValue("@Hours", emp.HoursWorked);
                cmd.Parameters.AddWithValue("@OT", emp.OverTime);
                cmd.Parameters.AddWithValue("@Leave", emp.LeaveDays);
                cmd.Parameters.AddWithValue("@NetPay", emp.NetPay);

                cmd.ExecuteNonQuery();
            }
        }

        public Employee GetEmployeeById(string empId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Employee WHERE empId = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", empId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Employee
                    {
                        EmpId = reader["empId"].ToString(),
                        EmpName = reader["empName"] == DBNull.Value ? null : reader["empName"].ToString(),
                        EmpTitle = reader["empTitle"] == DBNull.Value ? null : reader["empTitle"].ToString(),
                        HourlyRate = Convert.ToInt32(reader["hourlyRate"]),
                        HoursWorked = Convert.ToInt32(reader["hoursWorked"]),
                        OverTime = Convert.ToInt32(reader["overTime"]),
                        LeaveDays = Convert.ToInt32(reader["leaveDays"]),
                        NetPay = reader["netPay"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["netPay"])
                    };
                }
            }

            return null;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> list = new List<Employee>();

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Employee";
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Employee
                    {
                        EmpId = reader["empId"].ToString(),
                        EmpName = reader["empName"] == DBNull.Value ? null : reader["empName"].ToString(),
                        EmpTitle = reader["empTitle"] == DBNull.Value ? null : reader["empTitle"].ToString(),
                        HourlyRate = Convert.ToInt32(reader["hourlyRate"]),
                        HoursWorked = Convert.ToInt32(reader["hoursWorked"]),
                        OverTime = Convert.ToInt32(reader["overTime"]),
                        LeaveDays = Convert.ToInt32(reader["leaveDays"]),
                        NetPay = reader["netPay"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["netPay"])
                    });
                }
            }

            return list;
        }

        public void UpdateEmployee(Employee emp)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE Employee SET
                    empName=@Name,
                    empTitle=@Title,
                    hourlyRate=@Rate,
                    hoursWorked=@Hours,
                    overTime=@OT,
                    leaveDays=@Leave,
                    netPay=@NetPay
                    WHERE empId=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", emp.EmpId);
                cmd.Parameters.AddWithValue("@Name", emp.EmpName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Title", emp.EmpTitle ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Rate", emp.HourlyRate);
                cmd.Parameters.AddWithValue("@Hours", emp.HoursWorked);
                cmd.Parameters.AddWithValue("@OT", emp.OverTime);
                cmd.Parameters.AddWithValue("@Leave", emp.LeaveDays);
                cmd.Parameters.AddWithValue("@NetPay", emp.NetPay);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(string empId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM Employee WHERE empId=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", empId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
