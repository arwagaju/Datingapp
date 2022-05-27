using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class Accountcontroller : BaseApiController
    {

        private readonly DataContext _context;
        public Accountcontroller(DataContext context)
        {
            _context = context;
            
        }

        //method that registers a user
        [HttpPost("register")]

        public  async Task<ActionResult<AppUser>> Register (RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();

            //create a new user
            var user = new AppUser 
            {
                userName = registerDTO.userName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.password)), 
                PasswordSalt = hmac.Key
                

            };

            //add the user in the database
            _context.Users.Add(user);

            //save the changes 
            await _context.SaveChangesAsync();

            return user;
        }
        
    }
}