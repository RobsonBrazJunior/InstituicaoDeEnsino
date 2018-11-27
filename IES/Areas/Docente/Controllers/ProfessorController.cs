using IES.Data;
using IES.Data.DAL.Cadastros;
using IES.Data.DAL.Docente;
using Microsoft.AspNetCore.Mvc;
using Modelo.Cadastros;
using Modelo.Docente;
using System.Collections.Generic;

namespace IES.Areas.Docente.Controllers
{
    [Area("Docente")]
    public class ProfessorController : Controller
    {
        public readonly IESContext _context;
        public readonly InstituicaoDAL instituicaoDAL;
        public readonly DepartamentoDAL departamentoDAL;
        public readonly CursoDAL cursoDAL;
        public readonly ProfessorDAL professorDAL;

        public ProfessorController (IESContext context)
        {
            _context = context;
            instituicaoDAL = new InstituicaoDAL(context);
            departamentoDAL = new DepartamentoDAL(context);
            cursoDAL = new CursoDAL(context);
            professorDAL = new ProfessorDAL(context);
        }

        public void PrepararViewBags(List<Instituicao> instituicoes, List<Departamento> departamentos, List<Curso> cursos, List<Professor> professores)
        {
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;

            departamentos.Insert(0, new Departamento() { DepartamentoID = 0, Nome = "Selecione o departamento" });
            ViewBag.Departamentos = departamentos;

            cursos.Insert(0, new Curso() { CursoID = 0, Nome = "Selecione o curso" });
            ViewBag.Cursos = cursos;

            professores.Insert(0, new Professor() { ProfessorID = 0, Nome = "Selecione o professor" });
            ViewBag.Professores = professores;
        }
    }
}