using LearnAPI.Data;
using LearnAPI.Model.Learn;
using LearnAPI.Model.Learn.Request;
using LearnAPI.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LearnAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly LearnDbContext _context;
        private readonly IConfiguration _configuration;

        public ProgressController(LearnDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }
    }
}
