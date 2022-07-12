

using Microsoft.EntityFrameworkCore;
using WebApi6.Models;

namespace WebApi6.Data  

{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options) 
            : base(options)
        {

        }
        public  DbSet<Issue> Issues { get; set; }

    }

}
