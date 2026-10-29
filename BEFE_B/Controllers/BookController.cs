using BEFE_B.Models;
using Microsoft.AspNetCore.Mvc;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        static List<Book> books = new List<Book>();

        [HttpGet]
        public List<Book> GetBooks()
        {
            return books;
        }

        [HttpPost]
        public void CreateBook([FromBody] Book b)
        {
            books.Add(b);
        }
    }
}
