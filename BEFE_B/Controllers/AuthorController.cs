using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthorController(BookDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<AuthorViewDto> GetAuthors()
        {
            return _ctx.Authors.Select(z => _mapper.Map<AuthorViewDto>(z));
        }

        [HttpGet("{id}")]
        public AuthorViewDto GetAuthor(Guid id) 
        {
            return _mapper.Map<AuthorViewDto>(_ctx.Authors.Find(id));
        }

        [HttpPut("{id}")]
        public void UpdateAuthor([FromBody] AuthorCreateUpdateDto dto, Guid id)
        {
            var old = _ctx.Authors.Find(id);
            _mapper.Map(dto, old);
        }

        [HttpPost]
        public void CreateAuthor([FromBody] AuthorCreateUpdateDto dto)
        {
            _ctx.Authors.Add(_mapper.Map<Author>(dto));
            _ctx.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void DeleteAuthor(Guid id)
        {
            var authorToDelete = _ctx.Authors.First(t => t.Id == id);
            _ctx.Authors.Remove(authorToDelete);
            _ctx.SaveChanges();
        }
    }
}
