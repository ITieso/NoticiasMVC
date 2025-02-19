using ArchitectureMvc.Application.DTOs;
using ArchitectureMvc.Application.IServices;
using ArchitectureMvc.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArchitectureMvc.WebUI.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioDto usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _usuarioService.AddAsync(usuario);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Logar o erro (ex) conforme necessário
                    return Json(new { success = false, message = "Erro ao cadastrar o usuário. Tente novamente." });
                }
            }

            // Se o modelo não for válido, retornamos os erros de validação
            var errors = ModelState
                .Where(x => x.Value.Errors.Any())
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            return Json(new { success = false, errors });
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return View(usuarios);
        }

        // Exibir formulário de edição
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // Editar Usuário
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsuarioDto usuario)
        {

            if (ModelState.IsValid)
            {
                await _usuarioService.UpdateAsync(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // Exibir confirmação de exclusão
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // Excluir Usuário
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuarioService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
