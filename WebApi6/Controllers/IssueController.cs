using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi6.Data;
using WebApi6.Models;

namespace WebApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IssueDbContext _contextAccessor;
        public IssueController(IssueDbContext contextAccessor) => _contextAccessor = contextAccessor;

        [HttpGet]
        public async Task<IEnumerable<Issue>> Get() {
            return await _contextAccessor.Issues.ToListAsync();

        }
        [HttpGet("id")]
        [ProducesResponseType(typeof(Issue), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetId(int id) { 
            var issue = await _contextAccessor.Issues.FindAsync(id);
            return issue == null? NotFound() : Ok(issue);
           }

        [HttpPost]
        public async Task<IActionResult> Create(Issue issue) {
            await _contextAccessor.Issues.AddAsync(issue);
            await _contextAccessor.SaveChangesAsync();
            return CreatedAtAction(nameof(GetId), new {id = issue.id}, issue);
        }

        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id) {
            var findId = await _contextAccessor.Issues.FindAsync(id);  
            if (findId == null) return NotFound();
            _contextAccessor.Issues.Remove(findId);
            await _contextAccessor.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, Issue issue)
        {
            if (id != issue.id) return BadRequest();
            _contextAccessor.Entry(issue).State = EntityState.Modified;
            await _contextAccessor.SaveChangesAsync();
            return Ok();
        }

    }
}
