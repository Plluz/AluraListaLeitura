using System.IO;

namespace Alura.ListaLeitura.App.Utils
{
    public class HtmlUtils
    {
        public static string CarregarFormularioHTML(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"HTML/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
