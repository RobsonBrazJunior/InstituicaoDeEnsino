using IES.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IES.Controllers
{
    public class InstituicaoController : Controller
    {
        private static IList<Instituicao> instituicoes = new List<Instituicao>()
        {
            new Instituicao()
            {
                InstituicaoID = 1,
                Nome = "UniParaná",
                Endereco = "Paraná"
            },

            new Instituicao()
            {
                InstituicaoID = 2,
                Nome = "UniSanta",
                Endereco = "Santa Catarina"
            },

            new Instituicao()
            {
                InstituicaoID = 3,
                Nome = "UniSãoPaulo",
                Endereco = "São Paulo"
            },

            new Instituicao()
            {
                InstituicaoID = 4,
                Nome = "UniSulgrandense",
                Endereco = "Rio Grande do Sul"
            },

            new Instituicao()
            {
                InstituicaoID = 5,
                Nome = "UniCarioca",
                Endereco = "Rio de Janeiro"
            }
        };

        public IActionResult Index() => View(instituicoes);
    }
}