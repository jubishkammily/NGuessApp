using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NGAPI.Data;
using NGAPI.DTOs;
using NGAPI.Models;
using NGAPI.Services;


namespace NGAPI.Controllers;

    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly DataContext _dataContext;
        private ITokenService _tokenKService;
    public AccountController(DataContext dataContext,ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenKService = tokenService;
        }
         [AllowAnonymous]
         [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if (await UserExists(registerDto.UserName))
            {
                return BadRequest("User is taken");
            }
            var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenKService.GetToken(user)
            };
        
        }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
    {

            var user = await _dataContext.Users.FirstOrDefaultAsync(x=>x.UserName == loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }

            var hmac = new HMACSHA512(user.PasswordSalt);
            var computerHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computerHash.Length; i++)
            {
                if (computerHash[i] != user.PasswordHash[i])
                {
                    return Unauthorized("Invalid Username");

                }
            }
            return new UserDTO
            {
                UserName = user.UserName,
                Token = _tokenKService.GetToken(user)
            };

    }

    private async Task<bool> UserExists(string userName)
    {
        return await _dataContext.Users.AnyAsync(x => x.UserName == userName);
    }

}

