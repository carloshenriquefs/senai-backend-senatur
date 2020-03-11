using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using Senai.Senatur.WebApi.Repository;

namespace Senai.Senatur.WebApi.Controller
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PacoteController : ControllerBase
    {
        private IPacoteRepository _pacoteRepository;

        public PacoteController()
        {
            _pacoteRepository = new PacoteRepository();
        }

        /// <summary>
        /// Metodo para Listar Pacotes
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_pacoteRepository.Listar());
        }


        /// <summary>
        /// Metodo para buscar [ID] de um pacote
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_pacoteRepository.BuscarPorId(id));
        }

        /// <summary>
        /// Metodo para Cadastrar um Pacote
        /// </summary>
        /// <param name="novoPacote"></param>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(TblPacote novoPacote)
        {
            _pacoteRepository.Cadastrar(novoPacote);

            return StatusCode(201);
        }

        /// <summary>
        /// Metodo para Atualizar um Pacote
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pacoteAtualizado"></param>
        /// <returns></returns>
        [Authorize(Roles = "1")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, TblPacote pacoteAtualizado)
        {
            _pacoteRepository.Atualizar(id, pacoteAtualizado);

            return StatusCode(204);
        }

        /// <summary>
        /// Metodo para buscar pelos status 
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("Status/{status}")]
        public IActionResult GetByActive(bool status)
        {
            return Ok(_pacoteRepository.ListarPorAtivo(status));
        }

        /// <summary>
        /// Metodo para listar pacote por Cidade
        /// </summary>
        /// <param name="cidade"></param>
        /// <returns></returns>
        [HttpGet("Cidade/{cidade}")]
        public IActionResult GetByCity(string cidade)
        {
            return Ok(_pacoteRepository.ListarPorCidade(cidade));
        }

        /// <summary>
        /// Metodo para Ordenar por Preco
        /// </summary>
        /// <param name="ordem"></param>
        /// <returns></returns>
        [HttpGet("Ordem/{ordem}")]
        public IActionResult OrderByPrice(string ordem)
        {
            if(_pacoteRepository.OrdenarPorPreco(ordem) != null)
            {
                return Ok(_pacoteRepository.OrdenarPorPreco(ordem));
            }

            return NotFound("Tipo de ordenação inválido, use 'asc' ou 'desc'");
        }
    }
}