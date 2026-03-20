namespace EmployeeDataService
{
    public class Employee
    {
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public int HourlyRate { get; set; }
        public int HoursWorked { get; set; } = 80;
        public int OvertimeHours { get; set; } = 3;
        public double LeaveDays { get; set; } = 1;
    }
}
