using Comparativo_CLT_PJ.Enums;

public class SalaryBenefits : Salary
{
    public string Name { get; set; }
    public bool isDicounted { get; set; }
    public decimal AmountDiscounted { get; set; }
}