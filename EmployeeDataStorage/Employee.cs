namespace EmployeeDataService
{
    public class Employee
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpTitle { get; set; }
        public int HourlyRate { get; set; }
        public decimal HoursWorked { get; set; } = 80;
        public int OverTime { get; set; } = 3;
        public int LeaveDays { get; set; } = 1;
        public decimal NetPay { get; set; } = 0;
    }
}
