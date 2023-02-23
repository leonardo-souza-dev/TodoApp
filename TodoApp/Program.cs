using System.Text;

namespace TodoApp
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
    /// </summary>
    internal class Program
    {
        private static string _nomeArquivo = "tarefas.txt";

        static void Main(string[] args)
        {
            Console.WriteLine("TODO App");

            //
            var caminhoCompleto = Path.Combine(Environment.CurrentDirectory, _nomeArquivo);
            if (!File.Exists(caminhoCompleto))
            {
                File.Create(caminhoCompleto).Close();
            }
            //



            string escolha = String.Empty;

            while (escolha != "S" && escolha != "s")
            {
                Console.WriteLine("*** MENU ***");
                Console.WriteLine("[N] Nova tarefa");
                Console.WriteLine("[L] Listar tarefas");
                Console.WriteLine("[F] Finalizar tarefa");
                Console.WriteLine("[S] Sair");

                escolha = Console.ReadLine();

                switch (escolha)
                {
                    case "N":
                    case "n":
                        Console.WriteLine("Descrição da tarefa:");

                        string descricaoTarefa = Console.ReadLine();

                        Console.WriteLine("Nova tarefa criada: " + descricaoTarefa);

                        /*
                        using (FileStream fs = File.Create(_nomeArquivo))
                        {
                            Byte[] titulo = new UTF8Encoding(true).GetBytes("Tarefas:" + Environment.NewLine); //CR LF
                            fs.Write(titulo, 0, titulo.Length);
                            byte[] tarefa = new UTF8Encoding(true).GetBytes(descricaoTarefa);
                            fs.Write(tarefa, 0, tarefa.Length);
                        }
                        */
                        using (StreamWriter sw = File.AppendText(caminhoCompleto))
                        {
                            sw.WriteLine(descricaoTarefa);
                        }
                        break;
                    case "L":
                    case "l":
                        using (StreamReader sr = File.OpenText(caminhoCompleto))
                        {
                            string s = "";
                            while ((s = sr.ReadLine()) != null)
                            {
                                Console.WriteLine(s);
                            }
                        }
                        break;
                    case "F":
                    case "f":
                        Console.WriteLine("Digite a descrição da tarefa que deseja cancelar");

                        var descricaoTarefaCancelar = Console.ReadLine();

                        String[] linhas = File.ReadAllLines(caminhoCompleto);

                        for (int i = 0; i < linhas.Length; i++)
                        {
                            if (linhas[i] == descricaoTarefaCancelar)
                            {
                                linhas[i] = $"{linhas[i]};F";
                            }
                        }

                        File.WriteAllLines(caminhoCompleto, linhas);

                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Saindo...");            
        }
    }
}