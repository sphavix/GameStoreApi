using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record class UpdateGameDto(
    [Required][StringLength(25)] string Name,
    [Required][StringLength(25)] string Genre,
    [Required][Range(1, 1000)] decimal Price,
    DateOnly ReleaseDate
);
