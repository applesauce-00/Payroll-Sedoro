namespace EmployeeDataService
{
    public class Employee
    {
        public string EmpId { get; set; } // Primary Key
        public string EmpName { get; set; }
        public string EmpTitle { get; set; }
        public int Leave { get; set; }
        public Salary SalaryInfo { get; set; }
    }

    public class Salary
    {
        public string EmpId { get; set; } // Foreign Key
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal OverTimeHours { get; set; }
        public decimal OverTimePay { get; set; }
        public decimal Tax { get; set; }
        public decimal NetPay { get; set; }
    }
}
