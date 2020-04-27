using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Controller
{
    public class LivrosController
    {
        private static string CarregarLista(IEnumerable<Livro> livros)
        {
            var conteudoArquivo = HtmlUtils.CarregarFormularioHTML("para-ler");

            foreach (var livro in livros)
            {
                conteudoArquivo = conteudoArquivo.Replace("LISTA_AQUI", $"<li>{livro.Autor} ({livro.Titulo})</li>LISTA_AQUI");
            }

            return conteudoArquivo.Replace("LISTA_AQUI", "");
        }

        public static Task Detalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public static Task ParaLer(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var html = CarregarLista(repo.ParaLer.Livros);
            return context.Response.WriteAsync(html);
        }

        public static Task Lendo(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var html = CarregarLista(repo.Lendo.Livros);
            return context.Response.WriteAsync(html);
        }

        public string Lidos()
        {
            return CarregarLista(new LivroRepositorioCSV().Lidos.Livros);
        }

        public string Teste()
        {
            return "Nova funcionalidade...";
        }

    }
}
