using System;
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

            //check if the user exists
            if(UserExists(registerDTO.userName))
            {
                return BadRequest("The user already exists");
            }
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

        //login method 
        [HttpPost("login")]
        public ActionResult<AppUser> login(LoginDTO loginDTO)
        {
            //grab the user from the database
            var user = _context.Users.FirstOrDefault(x => x.userName == loginDTO.username);

            //check if the user has valid username
            if(user is null)
            {
                
                return Unauthorized("username is invalid");
            }

           
           using var hmac = new HMACSHA512(user.PasswordSalt);

           //compute password hash
           var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.password));

           //check if the password is the same from the stored one
           //if not, return unathorized
           for(int i = 0; i < passwordHash.Length; i++ )
           {
               if(passwordHash[i] != user.PasswordHash[i])
               {
                   
                   return Unauthorized("Invalid password");
               }

           }
           

           
            return user;
        }


        
        
        //method that checks if the user is already registered
        //compares username
        private bool UserExists(string username)
        {
            var user = _context.Users.FirstOrDefault(x => x.userName == username);

            if(user is not null)
            {
                return true;
            }

            return false;
            

        }

        
        
    }

    
}