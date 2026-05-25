using System.ComponentModel.DataAnnotations;

namespace ModaCircular.Api.DTOs;

public class CategoriaCreateDto
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 80 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(250, ErrorMessage = "A descrição deve ter no máximo 250 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    public bool Ativa { get; set; } = true;
}