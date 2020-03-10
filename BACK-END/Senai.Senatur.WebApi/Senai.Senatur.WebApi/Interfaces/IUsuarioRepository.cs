using Senai.Senatur.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Senatur.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        List<TblUsuario> Listar();

        TblUsuario BuscarPorEmailSenha(string email, string senha);

        //void BuscarPorEmailSenha(string email, string senha);
    }
}
