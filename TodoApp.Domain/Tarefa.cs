using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Domain
{
    public class Tarefa
    {
        public string Descricao { get; set; }
        public bool Finalizada { get; set; }

        public Tarefa(string descricao, bool finalizada)
        {
            Descricao = descricao;
            Finalizada = finalizada;
        }
        //default value. String => null, bool = false
    }
}
