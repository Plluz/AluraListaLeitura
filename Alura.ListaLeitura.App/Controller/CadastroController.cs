using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {
        public IActionResult FormularioCadastro()
        {
            var html = new ViewResult { ViewName = "formulario" };
            return html;
        }

        public string ConfirmarCadastro(Livro livro)
        {
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return "Livro cadastrado com sucesso!";
        }
    }
}
