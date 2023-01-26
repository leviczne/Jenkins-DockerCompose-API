using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SistemaDeCadastroAPI.Models.DTO_s;
using SistemaDeCadastroAPI.Models;
using SistemaDeCadastroAPI.Repositorios.Intefaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaDeCadastroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Security : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public Security(UserManager<ApplicationUser> userManager
            , SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
           
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioModel>> RegisterUser([FromBody] UsuarioDTO usuarioModel)
        {
            var user = new ApplicationUser
            {
                UserName = usuarioModel.Email,
                Email = usuarioModel.Email,
                ViajanteName = usuarioModel.ViajanteName,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, usuarioModel.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            await _signInManager.SignInAsync(user, false);
            return Ok(GeraToken(usuarioModel));

        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioModel>> Login([FromBody] UsuarioDTO userInfo)
        {
            var result = await _signInManager.PasswordSignInAsync(userInfo.Email,
                userInfo.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(GeraToken(userInfo));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido....");
                return BadRequest(ModelState);
            }

        }

        private UsuarioToken GeraToken(UsuarioDTO userInfo)
        {
            var claim = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName,userInfo.Email),
                new Claim("meuPet", "pipoca"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["TokenConfiguration:Issuer"],
                audience: _configuration["TokenConfiguration:Audience"],
                claims: claim,
                expires: expiration,
                signingCredentials: credenciais);

            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                Message = "Token JWT OK"
            };

        }


    }
}
