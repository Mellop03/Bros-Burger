using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrosBurger.Context;
using BrosBurger.Models;
using App.Filters;
using X.PagedList;
using System.Xml;
using System.Text;

namespace BrosBurger.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? Pesquisa, string? Ordenar, int pagina = 1)
        {
            int pageSize = 9;
            IQueryable<Produto> Lista = _context.Produtos.Include(p => p.Categoria);
            if (Pesquisa != null && Pesquisa != "")
            {
                Lista = Lista.Where(item => item.NomeProduto.ToLower().Contains(Pesquisa.ToLower()) || item.Categoria.NomeCategoria.ToLower().Contains(Pesquisa.ToLower()));
            }
            ViewData["Pesquisa"] = Pesquisa;
            if (Ordenar == "Ordem")
            {
                Lista = Lista.OrderBy(item => item.NomeProduto);
            }
            else if (Ordenar == "OrdemContrario")
            {
                Lista = Lista.OrderByDescending(item => item.NomeProduto);
            }
            ViewData["Ordenar"] = Ordenar;

            ProdutoBannerViewModel vm = new ProdutoBannerViewModel
            {
                ListaProdutos = Lista.ToPagedList(pagina, pageSize),
                ListaBanners = _context.Banners.ToList(),
                ListaCategorias = _context.Categorias.ToList()
            };

            return View(vm);
        }
    }
}