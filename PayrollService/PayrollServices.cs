using PayrollData;

namespace PayrollService
{
    public class PayrollServices
    {
        double overtimeIncrease = 1.25;
        double pagibig = 200 / 2;
        double sss = 500 / 2;
        double philhealth = 300 / 2;

        public bool IsValidEmployee(string inputId, string correctId)
        {
            return inputId == correctId;
        }

        public double GrossComputation(int rate, int hours)
        {
            return rate * hours;
        }

        public double OvertimeComputation(int rate, int hours)
        {
            return rate * (hours * overtimeIncrease);
        }

        public double LeaveDeduction(int rate, double leaveDays)
        {
            return rate * leaveDays;
        }

        public double TotalGross(double gross, double overtime, double leave)
        {
            return (gross + overtime) - leave;
        }

        public double TotalTax()
        {
            return pagibig + sss + philhealth;
        }

        public double NetPay(double totalGross)
        {
            return totalGross - TotalTax();
        }

        public (double Pagibig, double SSS, double Philhealth) GetTaxes()
        {
            return (pagibig, sss, philhealth);
        }
    }
}
