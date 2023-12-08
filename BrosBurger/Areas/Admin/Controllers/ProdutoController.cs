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

namespace BrosBurger.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class ProdutoController : Controller
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produto
        public IActionResult Index(string? botao, string? txtFiltro, string? selOrdenacao, int pagina = 1)
        {
            int pageSize = 6;
            IQueryable<Produto> Lista = _context.Produtos.Include(p => p.Categoria);
            if (botao == "relatorio"){
                pageSize = Lista.Count();
            }
            ViewData["txtFiltro"] = txtFiltro;
            if (txtFiltro != null && txtFiltro != ""){
                Lista = Lista.Where(item => item.NomeProduto.ToLower().Contains(txtFiltro.ToLower()) ||
                item.Categoria.NomeCategoria.ToLower().Contains(txtFiltro.ToLower()));
            }
            if (selOrdenacao == "Nome"){
                Lista = Lista.OrderBy(item => item.NomeProduto);
            } else if (selOrdenacao == "Nome_Desc"){
                Lista = Lista.OrderByDescending(item => item.NomeProduto);
            } else if (selOrdenacao == "Categoria"){
                Lista = Lista.OrderBy(item => item.Categoria.NomeCategoria);
            }
            ViewData["Ordem"] = selOrdenacao;
            if (botao == "XML")
            {
                return ExportarXML(Lista.ToList());
            } else if (botao == "Json")
            {
                //Chamando o método para exportar o Json enviando como parâmentro a lista já filtrada
                return ExportarJson(Lista.ToList());
            }
            return View(Lista.ToPagedList(pagina, pageSize));
        }
        private IActionResult ExportarXML(List<Produto> Lista)
        {
            var stream = new StringWriter();
            var xml = new XmlTextWriter(stream);

            xml.Formatting = System.Xml.Formatting.Indented;

            xml.WriteStartDocument();
            xml.WriteStartElement("Dados");

            xml.WriteStartElement("Produtos");
            foreach (var item in Lista)
            {
                xml.WriteStartElement("Produto");
                xml.WriteElementString("Id", item.ProdutoId.ToString());
                xml.WriteElementString("Nome", item.NomeProduto);
                xml.WriteElementString("Categoria", item.Categoria.NomeCategoria);
                xml.WriteElementString("Descrição", item.DescricaoProduto);
                xml.WriteElementString("Imagem", item.ImagemProduto);
                xml.WriteElementString("Valor", item.ValorProduto.ToString());
                xml.WriteEndElement(); // </Pais>
            }
            xml.WriteEndElement(); // </Paises>

            xml.WriteEndElement(); // </Data>
            return File(Encoding.UTF8.GetBytes(stream.ToString()), "application/xml", "dados_produtos.xml");
        }
        private IActionResult ExportarJson(List<Produto> Lista)
        {
            var json = new StringBuilder();
            json.AppendLine("{");
            json.AppendLine("  \"Produtos\": [");
            int total = 0;
            foreach (var item in Lista)
            {
                json.AppendLine("    {");
                json.AppendLine($"      \"Id\": {item.ProdutoId},");
                json.AppendLine($"      \"Nome\": \"{item.NomeProduto}\",");
                json.AppendLine($"      \"Categoria\": \"{item.Categoria.NomeCategoria}\",");
                json.AppendLine($"      \"Descrição\": \"{item.DescricaoProduto}\",");
                json.AppendLine($"      \"Imagem\": \"{item.ImagemProduto}\",");
                json.AppendLine($"      \"Valor\": {item.ValorProduto.ToString()}");
                json.AppendLine("    }");
                total++;
                if (total < Lista.Count())
                {
                    json.AppendLine("    ,");
                }
            }
            json.AppendLine("  ]");
            json.AppendLine("}");

            return File(Encoding.UTF8.GetBytes(json.ToString()), "application/json", "dados_produtos.json");
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produto/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "NomeCategoria");
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Produto produto)
        {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "CategoriaID", produto.CategoriaId);
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (true)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaID", "CategoriaID", produto.CategoriaId);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
          return (_context.Produtos?.Any(e => e.ProdutoId == id)).GetValueOrDefault();
        }
    }
}
