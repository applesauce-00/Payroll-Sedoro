using PayrollData;

namespace EmployeeDataService
{
    public class EmployeeInfos
    {
        public Employee GetEmployee()
        {
            return new Employee
            {
                EmpId = "kirby",
                Name = "Kirby T. Sedoro",
                Title = "Operation Manager",
                HourlyRate = 600,
                HoursWorked = 80,
                OvertimeHours = 3,
                LeaveDays = 1
            };
        }
    }
}
