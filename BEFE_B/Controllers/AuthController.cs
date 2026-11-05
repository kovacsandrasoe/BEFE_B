using AutoMapper;
using BEFE_B.Data;
using Microsoft.AspNetCore.Mvc;

namespace BEFE_B.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BookDbContext _ctx;
        private readonly IMapper _mapper;

        public AuthController(BookDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
    }
}
