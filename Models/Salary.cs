using Comparativo_CLT_PJ.Enums;

public class Salary
{
    public Periodicity Periodicity { get; set; }
    public decimal Amount { get; set; }
    public ContractType ContractRegime { get; set; }
    public Currency Currency { get; set; } = Currency.BRL;
    public decimal Benefits { get; set; }
    //public List<SalaryBenefits> Benefits { get; set; }

}
