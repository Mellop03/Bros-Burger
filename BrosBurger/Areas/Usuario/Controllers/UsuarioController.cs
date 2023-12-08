using Microsoft.AspNetCore.Mvc;
namespace Produtos_Com_Usuario.Areas.Usuario.Controllers
{
public class UsuarioController : Controller
{
[Area("Usuario")]
public IActionResult Index()
{
return View();
}
}
}