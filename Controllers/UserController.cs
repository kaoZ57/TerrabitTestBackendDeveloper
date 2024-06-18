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
    public class UserController : ControllerBase
    {
        private mydatabaseContext _dbContext;

        public UserController(mydatabaseContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpGet]
        public async Task<ActionResult<List<UserSearchResp>>> Search()
        {
            var result = await _dbContext.Users.ProjectTo<User, UserSearchResp>().ToListAsync();

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserReadResp>> Read(Guid id)
        {
            var result = await _dbContext.Users.Where(t => t.Id == id).ProjectTo<User, UserReadResp>().FirstOrDefaultAsync();

            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<UserReadResp>> Create(UserCreateReq request)
        {
            var result = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                IsActive = true,
                IsDelete = false,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            _dbContext.Users.Add(result);

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<UserReadResp>> Update(Guid id, UserCreateReq request)
        {
            var result = await _dbContext.Users.Where(t => t.Id == id).FirstOrDefaultAsync();

            if (result == null)
            {
                return BadRequest("User is emtyp");
            }

            result.FirstName = request.FirstName;
            result.LastName = request.LastName;
            result.Gender = request.Gender;

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _dbContext.Users.Where(t => t.Id == id).ProjectTo<User, UserReadResp>().FirstOrDefaultAsync();

            if (result == null)
            {
                return BadRequest("User is emtyp");
            }
            else
            {
                _dbContext.Users.Where(t => t.Id == id).ExecuteDelete();
            }

            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }
    }
}
