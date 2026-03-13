using EmployeeDataService;
namespace EmployeeDataStorage
{
    public class EmployeeInfo
    {
        public Employee GetEmployee()
        {
            return new Employee
            {
                EmpId = "kirby",
                Name = "Kirby T. Sedoro",
                Title = "Operation Manager",
                HourlyRate = 300,
                HoursWorked = 80,
                OvertimeHours = 3,
                LeaveDays = 1
            };
        }
    }
}
