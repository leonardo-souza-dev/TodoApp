using System.Text;
using TodoApp.Services;

namespace TodoApp.ConsoleApp
{
    /// <summary>
    /// 
    /// 
    /// => Criar um app para gerenciar tarefas. TODO App
    /// 
    /// CONTEÚDO
    /// Entrypoint C#: Contrato ou assinatura do método
    /// Padrão de classe vs nome do arquivo
    /// Parâmetros são opcionais: args 0 a n
    /// Como ler entrada do teclado/usuário
    /// Concatenar strings
    /// 
    /// 
    /// => Implementação do MENU para escolher NOVA TAREFA ou SAIR
    /// 
    /// CONTEÚDO
    /// Laço while
    /// Laço if
    /// Operadores lógicos AND e OR
    /// 
    /// 
    /// => Salvando arquivo de texto no disco para gravar as descrições das tarefas
    /// 
    /// CONTEÚDO
    /// File system
    /// String utils (CR LF)
    /// IL - Intermediate Language => C#, F#
    /// CLR - Common Language Runtime ("JVM")
    /// 
    /// 
    /// => Listar tarefas: lendo arquivo do disco, percorrendo o arquivo e imprimir na tela
    /// 
    /// CONTEÚDO
    /// StreamReader
    /// Environment.NewLine (agnóstico quanto a sistema operacional)
    /// 
    /// => Incrementar a gravação de novas tarefas: evitando que a cada nova tarefa ocorra a substituição da antiga
    /// 
    /// CONTEÚDO
    /// StreamWriter 
    /// ""Kafka, RabbitMQ => plataformas de Stream **MENSAGERIA**""
    /// 
    /// 
    /// => Finalizando tarefa
    /// 
    /// CONTEÚDO
    /// Return first / Resiliência
    /// Laço switch
    /// Path.Combine
    /// FileExists, FileCreate
    /// 
    /// 
    /// TAREFA DE CASA
    /// 1) Mudar a cor da fonte do console
    /// 2) Isolar a obtenção do caminho completo / método
    /// 3) Mensagem para quando não houver tarefas (ao listar)
    /// BONUS: Opcão no menu pra listagem de finalizadas e não-finalizadas
    /// 
    /// Contrai tudo (métodos)
    /// CTRL M + O 
    /// </summary>
    internal class Program
    {
        private static string _nomeArquivo = "tarefas.txt";

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("TODO App");

            CriarPreAjustes();

            ExibirMenu();

            Console.WriteLine("Saindo...");            
        }

        #region Métodos Privados

        private static void ExibirMenu()
        {
            string escolha = String.Empty;

            while (escolha != "S")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("*** MENU ***");
                Console.WriteLine("[N] Nova tarefa");
                Console.WriteLine("[L] Listar tarefas finalizadas");
                Console.WriteLine("[A] Listar tarefas em aberto");
                Console.WriteLine("[F] Finalizar tarefa");
                Console.WriteLine("[S] Sair");
                Console.ForegroundColor = ConsoleColor.Gray;

                escolha = Console.ReadLine().ToUpperInvariant();

                var tarefaService = new TarefaService();

                switch (escolha)
                {
                    case "N":
                        Console.WriteLine("Descrição da tarefa:");
                        string descricaoTarefa = Console.ReadLine();

                        var tarefa = tarefaService.CriarTarefa(descricaoTarefa);

                        Console.WriteLine("Nova tarefa criada: " + tarefa.Descricao);
                        break;
                    case "L": //Listar tarefas finalizadas

                        var tarefasFinalizadas = tarefaService.ListarTarefasFinalizadas();

                        if (tarefasFinalizadas.Any())
                        {
                            tarefasFinalizadas.ForEach(tf => Console.WriteLine(tf.Descricao));
                        }
                        else
                        {
                            Console.WriteLine("Não há tarefas finalizadas");
                        }

                        break;
                    case "A": //Listar tarefas em aberto
                        var tarefasEmAberto = tarefaService.ListarTarefasEmAberto();

                        if (tarefasEmAberto.Any())
                        {
                            tarefasEmAberto.ForEach(tf => Console.WriteLine(tf.Descricao));
                        }
                        else
                        {
                            Console.WriteLine("Não há tarefas em aberto");
                        }
                        break;
                    case "F":
                        Console.WriteLine("Digite a descrição da tarefa que deseja cancelar");
                        var descricaoTarefaCancelar = Console.ReadLine();
                        tarefaService.FinalizarTarefa(descricaoTarefaCancelar);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void CriarPreAjustes()
        {  
            if (!File.Exists(ObterCaminhoCompletoDoArquivo()))
            {
                File.Create(ObterCaminhoCompletoDoArquivo()).Close();
            }
        }

        private static string ObterCaminhoCompletoDoArquivo() 
            => Path.Combine(Environment.CurrentDirectory, _nomeArquivo);

        #endregion
    }
}