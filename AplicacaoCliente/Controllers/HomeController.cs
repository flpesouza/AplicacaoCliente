using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AplicacaoCliente.Models;
using AplicacaoCliente.Util;
using Newtonsoft.Json;

namespace AplicacaoCliente.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ClienteModel objCliente = new ClienteModel();
        ViewBag.ListaClientes = objCliente.ListarTodosClientes();

        return View();
    }

    [HttpGet]
    public IActionResult Registrar(int? id)
    {
        ClienteModel model = new ClienteModel();

        if (id != null)
        {
            model = new ClienteModel().Carregar(id);
        }
        else
        {
            model.Data_Cadastro = DateTime.Now;
        }
        return View(model);
    }

    public IActionResult Excluir(int id)
    {
        ViewData["ClienteID"] = id.ToString();
        return View();
    }

    public IActionResult ExcluirCliente(int id)
    {
        ClienteModel model = new ClienteModel();
        model.Excluir(id);

        return View("ExcluirCliente"); // chama a view que mostra "Cliente excluído com sucesso"
    }


    [HttpPost]
    public IActionResult Registrar(ClienteModel dados)
    {
        if (dados.Id == 0)
        {
            dados.Inserir(); // novo
        }
        else
        {
            dados.Atualizar(); // atualização - método que você pode criar depois
        }

        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
