using BEFE_B.Data;
using BEFE_B.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _ctx;

        public BookController(BookDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return _ctx.Books;
        }

        [HttpPost]
        public void CreateBook([FromBody] Book b)
        {
            _ctx.Books.Add(b);
            _ctx.SaveChanges();
        }
    }
}
