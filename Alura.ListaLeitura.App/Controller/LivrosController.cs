using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosController : Controller
    {
        public string Detalhes(int id)
        {
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return livro.Detalhes();
        }

        public IActionResult ParaLer()
        {
            var repo = new LivroRepositorioCSV();
            ViewBag.Livros = repo.ParaLer.Livros;
            return View("lista");
        }

        public IActionResult Lendo()
        {
            var repo = new LivroRepositorioCSV();
            ViewBag.Livros = repo.Lendo.Livros;
            return View("lista");
        }

        public IActionResult Lidos()
        {
            var repo = new LivroRepositorioCSV();
            ViewBag.Livros = repo.Lidos.Livros;
            return View("lista");
        }
    }
}
