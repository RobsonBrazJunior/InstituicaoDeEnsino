using IES.Data;
using IES.Data.DAL.Cadastros;
using Microsoft.AspNetCore.Mvc;

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
    }
}