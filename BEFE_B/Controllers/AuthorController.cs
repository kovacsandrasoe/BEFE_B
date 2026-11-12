using AutoMapper;
using BEFE_B.Data;
using BEFE_B.Dtos.Author;
using BEFE_B.Models;
using Hangfire;
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
        public async Task<List<AuthorViewDto>> GetAuthors()
        {
            return await 
                _ctx.Authors.Select(z => _mapper.Map<AuthorViewDto>(z)).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<AuthorViewDto> GetAuthor(Guid id) 
        {
            return _mapper.Map<AuthorViewDto>(await _ctx.Authors.FindAsync(id));
        }

        [HttpPut("{id}")]
        public async Task UpdateAuthor([FromBody] AuthorCreateUpdateDto dto, Guid id)
        {
            var old = _ctx.Authors.Find(id);
            _mapper.Map(dto, old);
            await _ctx.SaveChangesAsync();
            if (old != null)
            {
                BackgroundJob.Schedule(() => UpdateName(id), TimeSpan.FromSeconds(10));
            }

            //hangfire lehetőségek
            //aszinkron job: BackgroundService.Enqueue(job)
            //késleltetett job: BackgroundJob.Schedule(job, mikor)
            //ismétlődő job: RecurringJob.AddOrUpdate(job, cron kifejezés)
        }

        [NonAction]
        public void UpdateName(Guid id)
        {
            var author = _ctx.Authors.Find(id);
            author.Name = author.Name + "!";
            _ctx.SaveChanges();
        }

        [HttpPost]
        public async Task CreateAuthor([FromBody] AuthorCreateUpdateDto dto)
        {
            _ctx.Authors.Add(_mapper.Map<Author>(dto));
            await _ctx.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAuthor(Guid id)
        {
            var authorToDelete = _ctx.Authors.First(t => t.Id == id);
            _ctx.Authors.Remove(authorToDelete);
            await _ctx.SaveChangesAsync();
        }
    }
}
