using Api.Usuarios.Application.DTOs;
using Api.Usuarios.Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Usuarios.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost("autenticar")]
        public async Task<IActionResult> AutenticarAsync(AutenticarUsuarioRequest request)
        {
            try
            {
                var usuarioService = new UsuarioService();
                var response = await usuarioService.AutenticarUsuario(request);

                return Ok(response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(401, new { e.Message }); // HTTP UNAUTHORIZED (401)
            }
            catch(Exception e)
            {
                return StatusCode(500, new { e.Message }); // HTTP INTERNAL SERVER ERROR (500)
            }
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarAsync(CriarUsuarioRequest request)
        {
            try
            {
                var usuarioService = new UsuarioService();
                var response = await usuarioService.CriarUsuario(request);

                return StatusCode(201, response); // HTTP CREATED (201)
            }
            catch (ValidationException e)
            {
                return StatusCode(400, e.Errors.Select(e => e.ErrorMessage)); // HTTP BAD REQUEST (400)
            }
            catch (ApplicationException e)
            {
                return StatusCode(409, new { e.Message }); // HTTP CONFLICT (409)
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message }); // HTTP INTERNAL SERVER ERROR (500)
            }
        }
    }
}
