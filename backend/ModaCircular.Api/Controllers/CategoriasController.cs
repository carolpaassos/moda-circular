using Microsoft.AspNetCore.Mvc;
using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;
using ModaCircular.Api.Services;

namespace ModaCircular.Api.Controllers;

[ApiController]
[Route("categorias")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Categoria>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Categoria>>> Listar()
    {
        var categorias = await _categoriaService.ListarAsync();

        return Ok(categorias);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Categoria>> BuscarPorId(string id)
    {
        var categoria = await _categoriaService.BuscarPorIdAsync(id);

        if (categoria == null)
        {
            return NotFound(new
            {
                mensagem = "Categoria não encontrada."
            });
        }

        return Ok(categoria);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Categoria), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Categoria>> Criar(CategoriaCreateDto categoriaDto)
    {
        try
        {
            var categoria = await _categoriaService.CriarAsync(categoriaDto);

            return CreatedAtAction(nameof(BuscarPorId), new { id = categoria.Id }, categoria);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                mensagem = ex.Message
            });
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Atualizar(string id, CategoriaUpdateDto categoriaDto)
    {
        var atualizado = await _categoriaService.AtualizarAsync(id, categoriaDto);

        if (!atualizado)
        {
            return NotFound(new
            {
                mensagem = "Categoria não encontrada."
            });
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(string id)
    {
        var removido = await _categoriaService.RemoverAsync(id);

        if (!removido)
        {
            return NotFound(new
            {
                mensagem = "Categoria não encontrada."
            });
        }

        return NoContent();
    }
}