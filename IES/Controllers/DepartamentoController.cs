﻿using System.Linq;
using System.Threading.Tasks;
using IES.Data;
using IES.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IES.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _context;

        public DepartamentoController(IESContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.OrderBy(c => c.Nome).ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }

            return View(departamento);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return NotFound();

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);

            if (departamento == null)
                return NotFound();

            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID,Nome")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoID))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(departamento);
        }

        private bool DepartamentoExists(long? id) => _context.Departamentos.Any(e => e.DepartamentoID == id);

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
                return NotFound();

            var departamento = await _context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);

            if (departamento == null)
                return NotFound();

            return View(departamento);
        }
    }
}