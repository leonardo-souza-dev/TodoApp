using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Services;
using TodoApp.WebApp.Models;

namespace TodoApp.WebApp.Controllers
{ 
    public class HomeController : Controller
    {
        private readonly TarefaService _tarefaService;

        public HomeController()
        {
            _tarefaService = new TarefaService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("nova-tarefa")]
        public IActionResult NovaTarefa()
        {
            return View();
        }

        [Route("criar-tarefa")]
        public IActionResult CriarTarefa(string descricao) //ModelBinder
        {
            _tarefaService.CriarTarefa(descricao);

            return View("Index");
        }
    }
}