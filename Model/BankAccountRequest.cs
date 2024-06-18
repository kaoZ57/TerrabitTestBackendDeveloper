namespace TerrabitTest.Model;

public class BankAccountCreateReq
{
    public required int AccountType { get; set; }
}

public class BankAccountDepositReq : BankAccountMasterReq
{
}

public class BankAccountWithdrawalReq : BankAccountMasterReq
{
}

public class BankAccountTransferReq : BankAccountMasterReq
{
    public Guid UserIdTransfer { get; set; }
}

public class BankAccountMasterReq
{
    public decimal Balance { get; set; }
}


