namespace TerrabitTest.Model;

public class BankAccountReadResp
{
    public Guid Id { get; set; }
    public int AccountType { get; set; }
    public string AccountTypeName { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public DateTime OpenedDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public DateTime? DeleteDate { get; set; }
}