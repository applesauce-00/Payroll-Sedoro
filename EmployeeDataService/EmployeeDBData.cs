using Microsoft.Data.SqlClient;

namespace EmployeeDataService
{
    public class EmployeeDBData
    {
        private string connectionString =
            "Data Source=PIPS\\SQLEXPRESS;Initial Catalog=Payroll_Employee;Integrated Security=True;TrustServerCertificate=True;";

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
