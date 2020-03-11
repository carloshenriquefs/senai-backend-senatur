using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Senatur.WebApi.Interfaces;
using Senai.Senatur.WebApi.Repository;

namespace Senai.Senatur.WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private IUsuarioRepository _usuarioRepository;


        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Metodo para Listar Usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_usuarioRepository.Listar());
        }

       /// <summary>
       /// Metodo para buscar por email e senha de Usuarios
       /// </summary>
       /// <param name="email"></param>
       /// <param name="senha"></param>
       /// <returns></returns>
        [HttpGet("{email}")]
        public IActionResult GetByPasswordEmail(string email, string senha)
        {
            return Ok(_usuarioRepository.BuscarPorEmailSenha(email, senha));
        }
    }
}