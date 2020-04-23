using System;
using System.Collections.Generic;
using System.Text;
using Alura.ListaLeitura.App.Negocio;
using System.IO;
using System.Linq;

namespace Alura.ListaLeitura.App.Repositorio
{
    public class LivroRepositorioCSV : ILivroRepositorio
    {
        private static readonly string nomeArquivoCSV = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Repositorio\\livros.csv";

        public ListaDeLeitura ParaLer { get; }
        public ListaDeLeitura Lendo { get; }
        public ListaDeLeitura Lidos { get; }

        public LivroRepositorioCSV()
        {
            var arrayParaLer = new List<Livro>();
            var arrayLendo = new List<Livro>();
            var arrayLidos = new List<Livro>();

            using (var file = File.OpenText(LivroRepositorioCSV.nomeArquivoCSV))
            {
                while (!file.EndOfStream)
                {
                    var textoLivro = file.ReadLine();
                    if (string.IsNullOrEmpty(textoLivro))
                    {
                        continue;
                    }
                    var infoLivro = textoLivro.Split(';');
                    var livro = new Livro
                    {
                        Id = Convert.ToInt32(infoLivro[1]),
                        Titulo = infoLivro[2],
                        Autor = infoLivro[3]
                    };
                    switch (infoLivro[0])
                    {
                        case "para-ler":
                            arrayParaLer.Add(livro);
                            break;
                        case "lendo":
                            arrayLendo.Add(livro);
                            break;
                        case "lidos":
                            arrayLidos.Add(livro);
                            break;
                        default:
                            break;
                    }
                }
            }

            ParaLer = new ListaDeLeitura("Para Ler", arrayParaLer.ToArray());
            Lendo = new ListaDeLeitura("Lendo", arrayLendo.ToArray());
            Lidos = new ListaDeLeitura("Lidos", arrayLidos.ToArray());
        }

        public IEnumerable<Livro> Todos => ParaLer.Livros.Union(Lendo.Livros).Union(Lidos.Livros);

        public void Incluir(Livro livro)
        {
            var id = Todos.Select(l => l.Id).Max();
            using (var file = File.AppendText(LivroRepositorioCSV.nomeArquivoCSV))
            {
                file.WriteLine($"para-ler;{id+1};{livro.Titulo};{livro.Autor}");
            }
        }
    }
}
