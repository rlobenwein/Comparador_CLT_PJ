namespace Comparativo_CLT_PJ.Components.Services
{
    public interface ICalculatorService
    {
        public decimal CalculateNetCLT(Salary salary);
        public decimal CalculateNetPJ(Salary salary);
    }
}
