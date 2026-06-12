using EmployeeDataService;

namespace PayrollService
{
    public class PayrollComputation
    {
        private const decimal _pagibig = 100m;
        private const decimal _sss = 250m;
        private const decimal _philhealth = 150m;
        private const decimal OvertimeMultiplier = 1.25m;

        public PayrollResult ComputePayroll(Employee emp, Salary salary)
        {
            decimal gross = salary.HourlyRate * salary.HoursWorked;
            decimal otPay = salary.HourlyRate * salary.OverTimeHours * OvertimeMultiplier;

            decimal totalGross = gross + otPay;
            decimal totalTax = Pagibig() + SSS() + Philhealth();
            salary.Tax = totalTax;
            decimal netPay = totalGross - totalTax;


            salary.OverTimePay = otPay;
            salary.NetPay = netPay;

            return new PayrollResult
            {
                Gross = (double)gross,
                Overtime = (double)otPay,
                TotalGross = (double)totalGross,
                NetPay = (double)netPay
            };
        }

        public decimal TotalTax()
        {
            return Pagibig() + SSS() + Philhealth();
        }

        public decimal Pagibig()
        {
            return _pagibig;
        }

        public decimal SSS()
        {
            return _sss;
        }

        public decimal Philhealth()
        {
            return _philhealth;
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
