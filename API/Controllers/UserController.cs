using System.Reflection.Metadata.Ecma335;
using API.Data;
using API.Entity;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        // Database field
        private readonly DataContext _context;

        
        public  UserController(DataContext context)
        {
            _context = context;
            
        }

        //endpoint that gets all users

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();

        }
        
        //GETS A SPECIFIC USER WITH AN ID
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            if(id <= 0)
            {
                return NotFound();
            }

            return await _context.Users.FirstOrDefaultAsync(item => item.Id == id);


        }
        
    }
}