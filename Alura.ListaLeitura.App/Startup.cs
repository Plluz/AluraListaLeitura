using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Livros/Cadastrar/{autor}/{titulo}", LivrosCadastrar);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosDetalhes);
            builder.MapRoute("Livros/Cadastrar", FormularioLivorsCadastrar);
            builder.MapRoute("Livros/Cadastrar/Incluir", ProcessarFormulario);
            var rotas = builder.Build();
            app.UseRouter(rotas);
        }

        private Task ProcessarFormulario(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            var livro = new Livro()
            {
                Autor = context.Request.Query["autor"].First(),
                Titulo = context.Request.Query["titulo"].First()
            };
            repo.Incluir(livro);
            return context.Response.WriteAsync("Livro cadastrado com sucesso!");
        }

        private Task FormularioLivorsCadastrar(HttpContext context)
        {
            var html = @"
                    <html>
                        <form action='/Livros/Cadastrar/Incluir'>
                            <input name='autor'/>
                            <input name='titulo'/>
                            <button>Gravar</button>
                        </form>
                    </html>";
            return context.Response.WriteAsync(html);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        private Task LivrosCadastrar(HttpContext context)
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

        private Task LivrosDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.ParaLer.ToString());
        }

        public Task LivrosLendo(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(repo.Lidos.ToString());
        }
    }
}