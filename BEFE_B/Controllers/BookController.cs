using BEFE_B.Data;
using BEFE_B.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookDbContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;

        public BookController(BookDbContext ctx, UserManager<IdentityUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            var currentPrincipal = User;
            var currentUser = await _userManager.GetUserAsync(currentPrincipal);
            //todo do something with currentUser
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
