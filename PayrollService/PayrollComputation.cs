using EmployeeDataService;

namespace PayrollService
{
    public class PayrollComputation
    {
        private const double overtimeMultiplier = 1.25;
        private const double pagibig = 100;
        private const double sss = 250;
        private const double philhealth = 150;

        public PayrollResult ComputePayroll(Employee emp)
        {
            double gross = emp.HourlyRate * (double)emp.HoursWorked;
            double overtime = emp.HourlyRate * emp.OverTime * overtimeMultiplier;
            double leave = emp.HourlyRate * emp.LeaveDays;

            double totalGross = gross + overtime - leave;
            double totalTax = pagibig + sss + philhealth;

            return new PayrollResult
            {
                Gross = gross,
                Overtime = overtime,
                LeaveDeduction = leave,
                TotalGross = totalGross,
                NetPay = totalGross - totalTax
            };
        }

        public double TotalTax()
        {
            return pagibig + sss + philhealth;
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
