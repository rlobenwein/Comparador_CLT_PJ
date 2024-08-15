using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Comparativo_CLT_PJ.Components.Services
{
    public class CalculatorService : ICalculatorService
    {
        private const decimal EXEMPTION_LIMIT = 2112;
        private const decimal FIRST_RANGE_UPPER_LIMIT = 2826.65M;
        private const decimal SECOND_RANGE_UPPER_LIMIT = 3751.05M;
        private const decimal THIRD_RANGE_UPPER_LIMIT = 4664.68M;

        private const decimal FIRST_RANGE_PERCENTAGE = 0.075M;
        private const decimal SECOND_RANGE_PERCENTAGE = 0.15M;
        private const decimal THIRD_RANGE_PERCENTAGE = 0.225M;
        private const decimal FOURTH_RANGE_PERCENTAGE = 0.275M;
        private const int HOURS_PER_MONTH = 176; // 8 horas por dia * 22 dias por mês
        public decimal CalculateNetCLT(Salary salary)
        {
            decimal grossSalary = CalculateSalaryByMonth(salary);
            decimal incomeTaxes = CalculateTaxes(grossSalary);

            return grossSalary-incomeTaxes;
        }

        public decimal CalculateNetPJ(Salary salary)
        {
            throw new NotImplementedException();
        }
        private static decimal CalculateTaxes(decimal monthlySalary)
        {
            decimal firstRange;
            decimal secondRange;
            decimal thirdRange;
            if (monthlySalary <= EXEMPTION_LIMIT) return 0M;

            if (monthlySalary <= FIRST_RANGE_UPPER_LIMIT) return (monthlySalary - EXEMPTION_LIMIT) * FIRST_RANGE_PERCENTAGE;
            if (monthlySalary <= SECOND_RANGE_UPPER_LIMIT)
            {
                firstRange = (FIRST_RANGE_UPPER_LIMIT - EXEMPTION_LIMIT) * FIRST_RANGE_PERCENTAGE;
                secondRange = (monthlySalary - FIRST_RANGE_UPPER_LIMIT) * SECOND_RANGE_PERCENTAGE;
                return Math.Round(firstRange + secondRange, 2, MidpointRounding.AwayFromZero);
            }

            if (monthlySalary <= THIRD_RANGE_UPPER_LIMIT)
            {
                firstRange = (FIRST_RANGE_UPPER_LIMIT - EXEMPTION_LIMIT) * FIRST_RANGE_PERCENTAGE;
                secondRange = (SECOND_RANGE_UPPER_LIMIT - FIRST_RANGE_UPPER_LIMIT) * SECOND_RANGE_PERCENTAGE;
                thirdRange = (monthlySalary - SECOND_RANGE_UPPER_LIMIT) * THIRD_RANGE_PERCENTAGE;

                return Math.Round(firstRange + secondRange + thirdRange, 2, MidpointRounding.AwayFromZero);

            }
            firstRange = (FIRST_RANGE_UPPER_LIMIT - EXEMPTION_LIMIT) * FIRST_RANGE_PERCENTAGE;
            secondRange = (SECOND_RANGE_UPPER_LIMIT - FIRST_RANGE_UPPER_LIMIT) * SECOND_RANGE_PERCENTAGE;
            thirdRange = (THIRD_RANGE_UPPER_LIMIT - SECOND_RANGE_UPPER_LIMIT) * THIRD_RANGE_PERCENTAGE;
            decimal fourthRange = (monthlySalary - THIRD_RANGE_UPPER_LIMIT) * FOURTH_RANGE_PERCENTAGE;

            return Math.Round(firstRange + secondRange + thirdRange + fourthRange, 2, MidpointRounding.AwayFromZero);
        }
        private static decimal CalculateSalaryByMonth(Salary salary)
        {
            return salary.Periodicity switch
            {
                Enums.Periodicity.Hour => salary.Amount * HOURS_PER_MONTH,
                Enums.Periodicity.Year => salary.Amount / 12,
                _ => salary.Amount,
            };
        }
    }
}