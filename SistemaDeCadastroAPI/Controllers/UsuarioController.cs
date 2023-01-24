using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SistemaDeCadastroAPI.Models;
using SistemaDeCadastroAPI.Models.DTO_s;
using SistemaDeCadastroAPI.Repositorios.Intefaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SistemaDeCadastroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;


        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, UserManager<IdentityUser> userManager
            , SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _userManager = userManager;
            _signInManager = signInManager; 
            _configuration = configuration;
           
        }
  
        [HttpGet]
        public async Task<ActionResult<List<UsuarioModel>>> BuscasTodosUsuarios()
        {
          List<UsuarioModel> usuarios=  await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<List<UsuarioModel>>> BuscarPorId(int id)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarPorId(id);
            if (usuario == null)
            {
                throw new Exception("O usuário com este ID nao existe");
            }
            else
            {
                return Ok(usuario);
            }
        }

        [HttpGet("nome")]
        public async Task<ActionResult<UsuarioModel>> BuscarViagens(string nome)
        {
            UsuarioModel usuario = await _usuarioRepositorio.BuscarViagens(nome);
            List<String> NomesViagens = new List<String>(); 
            foreach (var Viagem in usuario.UsuariosViagems)
            {
                NomesViagens.Add(Viagem.IdViagemNavigation.NomeViagem);
            }

            return Ok(NomesViagens);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioModel>> Cadastrar([FromBody]UsuarioModel usuarioModel)
        {
           UsuarioModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);
           return Ok(usuario);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UsuarioModel>>RegisterUser([FromBody] UsuarioDTO usuarioModel)
        {
            var user = new IdentityUser
            {
                UserName = usuarioModel.Email,
                Email = usuarioModel.Email,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user,usuarioModel.Password);
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
                userInfo.Password,isPersistent:false,lockoutOnFailure:false);
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


        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioModel>> Atualizar([FromBody] UsuarioModel usuarioModel,int id)
        {
            usuarioModel.Id = id;
            UsuarioModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel,id);
            return Ok(usuario);
        }
       

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuarioModel>> Apagar(int id)
        {
            bool apagado = await _usuarioRepositorio.Apagar(id);
            return Ok(apagado);
        }

        

    }
}
