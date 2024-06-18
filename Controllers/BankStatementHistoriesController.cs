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
    public class BankStatementHistoryController : ControllerBase
    {
        private mydatabaseContext _dbContext;

        public BankStatementHistoryController(mydatabaseContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<BankStatementHistorySearchResp>>> Search(Guid id)
        {
            var result = await _dbContext.BankStatementHistories.Where(t => t.BankAccountId == id).ProjectTo<BankStatementHistory, BankStatementHistorySearchResp>().ToListAsync();

            return Ok(result);
        }
    }
}
