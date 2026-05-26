using Microsoft.AspNetCore.Mvc;
using ModaCircular.Api.DTOs;
using ModaCircular.Api.Models;
using ModaCircular.Api.Services;

namespace ModaCircular.Api.Controllers;

[ApiController]
[Route("roupas")]
public class RoupasController : ControllerBase
{
    private readonly IRoupaService _roupaService;

    public RoupasController(IRoupaService roupaService)
    {
        _roupaService = roupaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Roupa>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Roupa>>> Listar(
        [FromQuery] string? categoriaId,
        [FromQuery] string? tipo,
        [FromQuery] string? tamanho,
        [FromQuery] string? cidade)
    {
        var roupas = await _roupaService.ListarAsync(categoriaId, tipo, tamanho, cidade);

        return Ok(roupas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Roupa), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Roupa>> BuscarPorId(string id)
    {
        var roupa = await _roupaService.BuscarPorIdAsync(id);

        if (roupa == null)
        {
            return NotFound(new
            {
                mensagem = "Roupa não encontrada."
            });
        }

        return Ok(roupa);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Roupa), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Roupa>> Criar(RoupaCreateDto roupaDto)
    {
        try
        {
            var roupa = await _roupaService.CriarAsync(roupaDto);

            return CreatedAtAction(nameof(BuscarPorId), new { id = roupa.Id }, roupa);
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
    public async Task<IActionResult> Atualizar(string id, RoupaUpdateDto roupaDto)
    {
        try
        {
            var atualizado = await _roupaService.AtualizarAsync(id, roupaDto);

            if (!atualizado)
            {
                return NotFound(new
                {
                    mensagem = "Roupa não encontrada."
                });
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                mensagem = ex.Message
            });
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Remover(string id)
    {
        var removido = await _roupaService.RemoverAsync(id);

        if (!removido)
        {
            return NotFound(new
            {
                mensagem = "Roupa não encontrada."
            });
        }

        return NoContent();
    }
}