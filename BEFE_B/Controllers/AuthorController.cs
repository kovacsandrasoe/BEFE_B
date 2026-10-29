using BEFE_B.Data;
using BEFE_B.Models;
using Microsoft.AspNetCore.Mvc;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly BookDbContext _ctx;

        public AuthorController(BookDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<Author> GetAuthors()
        {
            return _ctx.Authors;
        }

        [HttpPost]
        public void CreateAuthor([FromBody] Author a)
        {
            _ctx.Authors.Add(a);
            _ctx.SaveChanges();
        }
    }
}
