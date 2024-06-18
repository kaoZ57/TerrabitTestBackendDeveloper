using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TerrabitTest.Core;
using TerrabitTest.Data;
using TerrabitTest.Model;

namespace TerrabitTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        private mydatabaseContext _dbContext;

        public BankAccountController(mydatabaseContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountReadResp>> Read(Guid id)
        {
            var result = await _dbContext.BankAccounts.Where(t => t.Id == id).ProjectTo<BankAccount, BankAccountReadResp>().FirstOrDefaultAsync();

            return Ok(result);
        }
        [HttpPost("{id}")]
        public async Task<ActionResult<BankAccountReadResp>> Create(Guid id, BankAccountCreateReq request)
        {
            var result = new BankAccount()
            {
                Id = id,
                AccountType = request.AccountType,
                Balance = 0,
                OpenedDate = DateTime.UtcNow,
                IsActive = true,
                IsDelete = false,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            _dbContext.BankAccounts.Add(result);

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }

        [HttpPost("Deposit/{id}")]
        public async Task<ActionResult<BankAccountReadResp>> Deposit(Guid id,BankAccountDepositReq request)
        {
            var result = await _dbContext.BankAccounts.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (result == null)
            {
                return BadRequest("User is emtyp");
            }

            result.Balance = result.Balance += request.Balance;

            var statementHistory = CreateBankStatementHistory(request,StatementType.Deposit, result.Id);
            _dbContext.BankStatementHistories.Add(statementHistory);

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }

        [HttpPost("Withdrawal/{id}")]
        public async Task<ActionResult<BankAccountReadResp>> Withdrawal(Guid id, BankAccountWithdrawalReq request)
        {
            var result = await _dbContext.BankAccounts.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (result == null)
            {
                return BadRequest("User is emtyp");
            }

            result.Balance = result.Balance -= request.Balance;

            var statementHistory = CreateBankStatementHistory(request, StatementType.Withdrawal, result.Id);
            _dbContext.BankStatementHistories.Add(statementHistory);

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }

        [HttpPost("Transfer/{id}")]
        public async Task<ActionResult<BankAccountReadResp>> Transfer(Guid id, BankAccountTransferReq request)
        {
            var resultFrom = await _dbContext.BankAccounts.Where(t => t.Id == id).FirstOrDefaultAsync();

            var resultTo = await _dbContext.BankAccounts.Where(t => t.Id == request.UserIdTransfer).FirstOrDefaultAsync();

            if (resultFrom == null || resultTo == null)
            {
                return BadRequest("User is emtyp");
            }

            resultFrom.Balance = resultFrom.Balance -= request.Balance;
            resultTo.Balance = resultTo.Balance += request.Balance;

            var statementHistory = CreateBankStatementHistory(request, StatementType.Transfer, resultFrom.Id,resultTo.Id);
            _dbContext.BankStatementHistories.Add(statementHistory);

            await _dbContext.SaveChangesAsync();

            return Ok(resultFrom);
        }


        [HttpPost("Close/{id}")]
        public async Task<ActionResult<BankAccountReadResp>> Close(Guid id)
        {
            var result = await _dbContext.Users.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (result == null)
            {
                return BadRequest("User is emtyp");
            }

            result.IsActive = false;

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }

        #region Tool
        public BankStatementHistory CreateBankStatementHistory<T>(T request, StatementType statementType, Guid bankAccountId, Guid? bankAccountTransferToId = null) where T : BankAccountMasterReq
        {
            var result = new BankStatementHistory()
            {
                Id = Guid.NewGuid(),
                BankAccountId = bankAccountId,
                Balance = request.Balance,
                StatementType = (int)statementType,
                IsActive = true,
                IsDelete = false,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            return result;
        }
        #endregion
    }
}
