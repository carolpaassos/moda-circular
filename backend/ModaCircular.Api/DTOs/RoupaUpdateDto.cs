using System.ComponentModel.DataAnnotations;

namespace ModaCircular.Api.DTOs;

public class RoupaUpdateDto
{
    [Required(ErrorMessage = "O título da roupa é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 100 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição da roupa é obrigatória.")]
    [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo é obrigatório. Informe Troca ou Doação.")]
    public string Tipo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tamanho é obrigatório.")]
    public string Tamanho { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado de conservação é obrigatório.")]
    public string EstadoConservacao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O gênero é obrigatório.")]
    public string Genero { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado é obrigatório.")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "A cidade é obrigatória.")]
    public string Cidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O bairro é obrigatório.")]
    public string Bairro { get; set; } = string.Empty;

    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public string CategoriaId { get; set; } = string.Empty;

    public bool Disponivel { get; set; } = true;
}