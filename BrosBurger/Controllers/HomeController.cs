using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BrosBurger.Models;
using BrosBurger.Context;
using X.PagedList;

namespace BrosBurger.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? Pesquisa, string? Ordenar, int pagina = 1)
        {
            int pageSize = 9;
            IQueryable<Produto> Lista = _context.Produtos.Include(p => p.Categoria);
            if (Pesquisa != null && Pesquisa != ""){
                Lista = Lista.Where(item => item.NomeProduto.ToLower().Contains(Pesquisa.ToLower()) || item.Categoria.NomeCategoria.ToLower().Contains(Pesquisa.ToLower()));
            }
            ViewData["Pesquisa"] = Pesquisa;
            if (Ordenar == "Ordem"){
                Lista = Lista.OrderBy(item => item.NomeProduto);
            } else if (Ordenar == "OrdemContrario")
            ViewData["Ordenar"] = Ordenar;
            ProdutoBannerViewModel vm = new ProdutoBannerViewModel
            {
                ListaProdutos = Lista.ToPagedList(pagina, pageSize),
                ListaBanners = _context.Banners.ToList(),
                ListaCategorias = _context.Categorias.ToList()
            };
            return View(vm);
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
