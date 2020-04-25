using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            var rotas = builder.Build();
            app.UseRouter(rotas);
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