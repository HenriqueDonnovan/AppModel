using AppModel.Data;
using AppModel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AppModel.Controllers
{
    public class TesteCrudController : Controller
    {
        private readonly MeuDbContext _contexto;

        public TesteCrudController(MeuDbContext contexto)
        {
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            var aluno = new Aluno
            {
                Name = "Eduardo",
                BirthDate = DateTime.Now,
                Email = "dudu@net.br"
            };

            _contexto.Alunos.Add(aluno);
            _contexto.SaveChanges();

            var aluno2 = _contexto.Alunos.Find(aluno.Id);

            var aluno3 = _contexto.Alunos.FirstOrDefault(aluno => aluno.Email == "dudu@net.br");
            var aluno4 = _contexto.Alunos.Where(a => a.Name == "Eduardo");

            aluno.Name = "João";
            _contexto.Alunos.Update(aluno);
            _contexto.SaveChanges();

            _contexto.Alunos.Remove(aluno);
            _contexto.SaveChanges();

            return View("_Layout");
        }
    }
}
