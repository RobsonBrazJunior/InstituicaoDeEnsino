﻿using System.Threading.Tasks;
using IES.Data;
using IES.Data.DAL.Discente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelo.Discente;

namespace IES.Areas.Discente.Controllers
{
    public class AcademicoController : Controller
    {
        private readonly IESContext _context;
        private readonly AcademicoDAL academicoDAL;

        public AcademicoController(IESContext context)
        {
            _context = context;
            academicoDAL = new AcademicoDAL(context);
        }

        public async Task<IActionResult> Index() => View(await academicoDAL.ObterAcademicosClassificadosPorNome().ToListAsync());

        private async Task<IActionResult> ObterVisaoAcademicoPorId(long? id)
        {
            if (id == null)
                return NotFound();

            var academico = await academicoDAL.ObterAcademicoPorId((long)id);

            if (academico == null)
                return NotFound();

            return View(academico);
        }

        public async Task<IActionResult> Details(long? id) => await ObterVisaoAcademicoPorId(id);

        public async Task<IActionResult> Edit(long? id) => await ObterVisaoAcademicoPorId(id);

        public async Task<IActionResult> Delete(long? id) => await ObterVisaoAcademicoPorId(id);

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,RegistroAcademico,Nascimento")] Academico academico)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await academicoDAL.GravarAcademico(academico);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch(DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados!");
            }

            return View(academico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("AcademicoID,Nome,RegistroAcademico,Nascimento")] Academico academico)
        {
            if (id != academico.AcademicoID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await academicoDAL.GravarAcademico(academico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicoExists(academico.AcademicoID))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(academico);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var academico = await academicoDAL.EliminarAcademicoPorId((long)id);
            TempData["Message"] = "Acadêmico" + academico.Nome.ToUpper() + " foi removida";
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AcademicoExists(long? id) => await academicoDAL.ObterAcademicoPorId((long)id) != null;
    }
}