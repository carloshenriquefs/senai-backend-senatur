using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using Senai.Senatur.WebApi.Repository;
using Senai.Senatur.WebApi.ViewModel;

namespace Senai.Senatur.WebApi.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        IUsuarioRepository _usuarioRepository { get; set; }


    public LoginController()
    {
         _usuarioRepository = new UsuarioRepository();
    }
    
    [HttpGet("{email}")]
    public IActionResult GetByPasswordEmail(string email, string senha)
    {
         return Ok(_usuarioRepository.BuscarPorEmailSenha(email, senha));
    }


        
    [HttpPost]
    public IActionResult Post(LoginViewModel login)
        {
            TblUsuario usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);

            //if(usuarioBuscado == null)
            //{
            //    return NotFound("Usuario não Cadastrado!");
            //}
            //if(usuarioBuscado.Senha != login.Senha && usuarioBuscado.Email != login.Email)
            //{
            //    return NotFound("Usuario ou senha invalidos!");
            //} 

            //return Ok();



            if(usuarioBuscado == null)
            {
                return NotFound("E-mail ou senha inválidos!");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                new Claim(ClaimTypes.Role, usuarioBuscado.FkIdTipoUsuario.ToString())
            };

            // Define a chave de acesso ao token
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("InLock-chave-autenticacao"));

            // Define as credenciais do token - Header
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gera o token
            var token = new JwtSecurityToken(
                issuer: "InLock.WebApi",                 // emissor do token
                audience: "InLock.WebApi",               // destinatário do token
                claims: claims,                          // dados definidos acima
                expires: DateTime.Now.AddMinutes(30),    // tempo de expiração
                signingCredentials: creds                // credenciais do token
            );

            // Retorna Ok com o token
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });

        }

    }

}