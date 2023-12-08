
using X.PagedList;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrosBurger.Models
{
    public class ProdutoBannerViewModel
    {
        public IPagedList<Produto> ListaProdutos { get; set; }
        public IEnumerable<Banners> ListaBanners { get; set; }
        public IEnumerable<Categoria> ListaCategorias { get; set; }
    }
}