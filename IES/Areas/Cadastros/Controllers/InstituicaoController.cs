﻿using IES.Data;
using Modelo.Cadastros;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using IES.Data.DAL.Cadastros;
using Microsoft.AspNetCore.Authorization;

namespace IES.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    [Authorize]
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;
        private readonly InstituicaoDAL instituicaoDAL;

        public InstituicaoController(IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
        }

        private async Task<IActionResult> ObterVisoaInstituicaoPorId(long? id)
        {
            if (id == null)
                return NotFound();

            var instituicao = await instituicaoDAL.ObterInstituicaoPorId((long) id);

            if (instituicao == null)
                return NotFound();

            return View(instituicao);
        }

        public async Task<IActionResult> Index() => View(await instituicaoDAL.ObterInstituicoesClassificadasPorNome().ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Endereco")] Instituicao instituicao)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    await instituicaoDAL.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }

            return View(instituicao);
        }

        public async Task<IActionResult> Edit(long? id) => await ObterVisoaInstituicaoPorId(id);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID,Nome,Endereco")] Instituicao instituicao)
        {
            if (id != instituicao.InstituicaoID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                   await instituicaoDAL.GravarInstituicao(instituicao);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await InstituicaoExists(instituicao.InstituicaoID))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(instituicao);
        }

        private async Task<bool> InstituicaoExists(long? id)
        {
            return await instituicaoDAL.ObterInstituicaoPorId((long) id) != null;
        }

        public async Task<IActionResult> Details(long? id) => await ObterVisoaInstituicaoPorId(id);

        public async Task<IActionResult> Delete(long? id) => await ObterVisoaInstituicaoPorId(id);

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await instituicaoDAL.EliminarInstituicaoPorId((long) id);
            TempData["Message"] = "Instituição " + instituicao.Nome.ToUpper() + " foi removida";
            return RedirectToAction(nameof(Index));
        }
    }
}