using Senai.Senatur.WebApi.Domains;
using Senai.Senatur.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SenaturContext ctx = new SenaturContext();

       

        //public void BuscarPorEmailSenha(string email, string senha)
        //{
        //   return ctx.TblUsuario.FirstOrDefault(u => u.Email == email && u.Senha == senha );

        //}

        public List<TblUsuario> Listar()
        {
            return ctx.TblUsuario.ToList();
        }

        TblUsuario IUsuarioRepository.BuscarPorEmailSenha(string email, string senha)
        {
            TblUsuario usuariobuscado = ctx.TblUsuario.Find(email, senha);


            return usuariobuscado;
        }
    }


}
