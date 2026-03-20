using EmployeeDataService;

namespace PayrollService
{
    public class PayrollComputation
    {
        private double overtimeMultiplier = 1.25;
        private double pagibig = 100;
        private double sss = 250;
        private double philhealth = 150;


        EmployeeDBData employeeDBData = new EmployeeDBData();


        public double ComputeGross(int rate, int hours)
        {
            return rate * hours;
        }
        public double ComputeOvertime(int rate, int hours)
        {
            return rate * hours * overtimeMultiplier;
        }
        public double ComputeLeaveDeduction(int rate, double leave)
        {
            return rate * leave;
        }
        public double ComputeTotalGross(double gross, double overtime, double leave)
        {
            return (gross + overtime) - leave;
        }
        public double TotalTax()
        {
            return pagibig + sss + philhealth;
        }
        public double ComputeNetPay(double totalGross)
        {
            return totalGross - TotalTax();
        }
        public double Pagibig()
        {
            return pagibig;
        }
        public double SSS()
        {
            return sss;
        }
        public double Philhealth()
        {
            return philhealth;
        }
        public PayrollResult ComputePayroll(Employee emp)
        {
            double gross = ComputeGross(emp.HourlyRate, (int)emp.HoursWorked);
            double overtime = ComputeOvertime(emp.HourlyRate, emp.OverTime);
            double leave = ComputeLeaveDeduction(emp.HourlyRate, emp.LeaveDays);
            double totalGross = ComputeTotalGross(gross, overtime, leave);
            double netPay = ComputeNetPay(totalGross);

            return new PayrollResult
            {
                Gross = gross,
                Overtime = overtime,
                LeaveDeduction = leave,
                TotalGross = totalGross,
                NetPay = netPay
            };
        }
    }

    public class PayrollResult
    {
        public double Gross { get; set; }
        public double Overtime { get; set; }
        public double LeaveDeduction { get; set; }
        public double TotalGross { get; set; }
        public double NetPay { get; set; }
    }
}
