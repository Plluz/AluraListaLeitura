using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
        }

        public Task Roteamento(HttpContext context)
        {
            var repo = new LivroRepositorioCSV();

            var caminhosAtendidos = new Dictionary<string, string>
            {
                {"/Livros/ParaLer", repo.ParaLer.ToString() },
                {"/Livros/Lendo", repo.Lendo.ToString() },
                {"/Livros/Lidos", repo.Lidos.ToString() }
            };

            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                return context.Response.WriteAsync(caminhosAtendidos[context.Request.Path]);
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync($"Caminho '{context.Request.Path}' inexistente!");
        }
    }
}