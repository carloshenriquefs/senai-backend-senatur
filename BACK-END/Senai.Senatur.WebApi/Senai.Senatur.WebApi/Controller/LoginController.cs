using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        IUsuarioRepository _usuarioRepository;


    public LoginController()
    {
            _usuarioRepository = new UsuarioRepository();
    }

    [HttpPost]
    public IActionResult Post(LoginViewModel login)
        {
            TblUsuario usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.Senha, login.Email);

            if(usuarioBuscado == null)
            {
                return NotFound("Usuario não Cadastrado!");
            }
            //if(usuarioBuscado.Senha != login.Senha && usuarioBuscado.Email != login.Email)
            //{
            //    return NotFound("Usuario ou senha invalidos!");
            //} 

            return Ok();

        }

    }

}