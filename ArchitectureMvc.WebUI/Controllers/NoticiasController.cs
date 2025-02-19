using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Application.IServices;
using ArchitectureMvc.Application.Services;
using ArchitectureMvc.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ArchitectureMvc.WebUI.Controllers
{
    public class NoticiaController : Controller
    {
        private readonly INoticiaService _noticiaService;
        private readonly IUsuarioService _usuarioService;
        public NoticiaController(INoticiaService noticiaService, IUsuarioService usuarioService)
        {
            _noticiaService = noticiaService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var noticias = await _noticiaService.GetAllAsync();
            var usuarios = await _usuarioService.GetAllAsync();
            var usuarioMap = usuarios.ToDictionary(u => u.Id, u => u.Nome);
            foreach (var noticia in noticias)
            {
                noticia.UsuarioNome = usuarioMap.ContainsKey(noticia.UsuarioId) ? usuarioMap[noticia.UsuarioId] : "Usuário Desconhecido";
            }
            return View(noticias);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();

            var noticiaDto = await _noticiaService.GetByIdAsync(id);
            if (noticiaDto == null)
                return NotFound();

            return View(noticiaDto);
        }

        public async Task<IActionResult> Create()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nome");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] NoticiaDto noticiaDto)
        {
            if (ModelState.IsValid)
            {
                await _noticiaService.AddAsync(noticiaDto);
                return Json(new { success = true });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }
     
        public ActionResult Delete(int id)
        {
            // Lógica para obter a notícia pelo ID
            var noticia = _noticiaService.GetByIdAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            return View(noticia);
        }

        
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _noticiaService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var noticia = await _noticiaService.GetByIdAsync(id);
            if (noticia == null)
            {
                return NotFound();
            }
            var usuarios = await _usuarioService.GetAllAsync();
            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nome", noticia.UsuarioId);

            return View(noticia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[FromForm] NoticiaDto noticia)
        {

            try
            {
                noticia.Id = id;
                await _noticiaService.UpdateAsync(noticia);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = new List<string> { ex.Message } });
            }
        }
    }
}

