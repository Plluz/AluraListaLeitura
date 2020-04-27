using Alura.ListaLeitura.App.Logica;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosExibicao.LivrosDetalhes);
            builder.MapRoute("Livros/ParaLer", LivrosExibicao.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosExibicao.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosExibicao.LivrosLidos);
            builder.MapRoute("Livros/Cadastrar/{autor}/{titulo}", LivrosCadastro.LivrosCadastrar);
            builder.MapRoute("Livros/Cadastrar", LivrosCadastro.FormularioLivrosCadastrar);
            builder.MapRoute("Livros/Cadastrar/Incluir", LivrosCadastro.ProcessarFormulario);
            var rotas = builder.Build();
            app.UseRouter(rotas);
        }

        public void ConfigureServices(IServiceCollection services) => services.AddRouting();
    }
}