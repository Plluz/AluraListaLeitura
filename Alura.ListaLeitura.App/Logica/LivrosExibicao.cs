using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class LivrosExibicao
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

        public static Task LivrosDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var html = CarregarLista(repo.ParaLer.Livros);
            return context.Response.WriteAsync(html);
        }

        public static Task LivrosLendo(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var html = CarregarLista(repo.Lendo.Livros);
            return context.Response.WriteAsync(html);
        }

        public static Task LivrosLidos(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var html = CarregarLista(repo.Lidos.Livros);
            return context.Response.WriteAsync(html);
        }
    }
}
