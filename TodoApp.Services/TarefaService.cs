using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain;

namespace TodoApp.Services
{
    // arquitetura hexagonal, 3 tier
    public class TarefaService
    {
        public Tarefa CriarTarefa(string descricaoTarefa)
        {
            var tarefa = new Tarefa(descricaoTarefa, false);

            using (StreamWriter sw = File.AppendText(ObterCaminhoCompletoDoArquivo()))
            {
                sw.WriteLine($"{tarefa.Descricao};{(tarefa.Finalizada ? 1 : 0)}");
            }

            return tarefa;
        }

        private static string ObterCaminhoCompletoDoArquivo()
            => Path.Combine(Environment.CurrentDirectory, "tarefas.txt");

        public List<Tarefa> ListarTarefasFinalizadas()
        {
            var tarefas = new List<Tarefa>();

            using (StreamReader sr = File.OpenText(ObterCaminhoCompletoDoArquivo()))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    var tarefaSplit = s.Split(";"); //Estudar;F

                    if (tarefaSplit.Length == 2 && tarefaSplit[1] == "1")
                    {
                        var tarefa = new Tarefa(tarefaSplit[0], tarefaSplit[1] == "1");
                        tarefas.Add(tarefa);
                    }
                }
            }

            return tarefas;
        }

        public List<Tarefa> ListarTarefasEmAberto()
        {
            var tarefasEmAberto = new List<Tarefa>();

            using (StreamReader sr = File.OpenText(ObterCaminhoCompletoDoArquivo()))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    var tarefaSplit = s.Split(";");
                    if (tarefaSplit.Length == 2 && tarefaSplit[1] == "0")
                    {
                        var tarefa = new Tarefa(tarefaSplit[0], tarefaSplit[1] == "0");
                        tarefasEmAberto.Add(tarefa);
                    }
                }
            }

            return tarefasEmAberto;
        }

        public void FinalizarTarefa(string descricaoTarefaCancelar)
        {
            if (string.IsNullOrEmpty(descricaoTarefaCancelar))
            {
                throw new ArgumentNullException(nameof(descricaoTarefaCancelar)); // magic string
            }

            String[] linhas = File.ReadAllLines(ObterCaminhoCompletoDoArquivo());

            var tarefas = new List<Tarefa>();
            foreach (var linha in linhas)
            {
                var linhaSplit = linha.Split(";");
                var finalizada = linhaSplit[1] == "1";
                var tarefaLinha = new Tarefa(linhaSplit[0], finalizada);
                tarefas.Add(tarefaLinha);
            }

            //atualizar tarefa para finalizada
            for (int i = 0; i < tarefas.Count; i++)
            {
                if (tarefas[i].Descricao == descricaoTarefaCancelar)
                {
                    tarefas[i].Finalizada = true;
                }
            }

            // gravar arquivo com tarefas atualizadas
            var linhasGravar = new List<string>();
            foreach (var t in tarefas)
            {
                //var finalizadaTexto = t.Finalizada ? 1 : 0;
                linhasGravar.Add($"{t.Descricao};{(t.Finalizada ? 1 : 0)}");
            }

            File.WriteAllLines(ObterCaminhoCompletoDoArquivo(), linhasGravar);

        }
    }
}
