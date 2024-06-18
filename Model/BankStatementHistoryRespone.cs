namespace TerrabitTest.Model;

public class BankStatementHistoryReadResp
{
    public Guid Id { get; set; }
    public Guid BankAccountId { get; set; }
    public int StatementType { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
}