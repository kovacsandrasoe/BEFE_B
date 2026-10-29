using BEFE_B.Data;
using BEFE_B.Dtos.Author;
using BEFE_B.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IEnumerable<AuthorViewDto> GetAuthors()
        {
            return _ctx.Authors.Select(z => new AuthorViewDto()
            {
                Id = z.Id,
                Name = z.Name,
                BirthDate = z.BirthDate,
                TitleOfBooks = z.Books != null ? z.Books.Select(y => y.Title).ToList() : new List<string>()
            });
        }

        [HttpPost]
        public void CreateAuthor([FromBody] AuthorCreateUpdateDto dto)
        {
            var a = new Author();
            a.Name = dto.Name;
            a.BirthDate = dto.BirthDate;

            _ctx.Authors.Add(a);
            _ctx.SaveChanges();
        }
    }
}
