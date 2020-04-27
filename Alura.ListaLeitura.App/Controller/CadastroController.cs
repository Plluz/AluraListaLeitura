using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Alura.ListaLeitura.App.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Controller
{
    public class CadastroController
    {
        public static Task ProcessarCadastro(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var livro = new Livro()
            {
                Autor = context.Request.Form["autor"].First(),
                Titulo = context.Request.Form["titulo"].First()
            };
            repo.Incluir(livro);
            return context.Response.WriteAsync("Livro cadastrado com sucesso!");
        }

        public static Task FormularioCadastro(HttpContext context)
        {
            var html = HtmlUtils.CarregarFormularioHTML("cadastrar");
            return context.Response.WriteAsync(html);
        }

        public static Task Cadastrar(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var livro = new Livro()
            {
                Autor = context.GetRouteValue("autor").ToString(),
                Titulo = context.GetRouteValue("titulo").ToString()
            };
            repo.Incluir(livro);
            return context.Response.WriteAsync("Livro cadastrado com sucesso!");
        }

    }
}
