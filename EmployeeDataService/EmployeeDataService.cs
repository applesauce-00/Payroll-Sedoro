namespace EmployeeDataService
{
    public class Employee
    {
        public string EmpId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }

        public int HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public int OvertimeHours { get; set; }

        public double LeaveDays { get; set; }
    }

}
