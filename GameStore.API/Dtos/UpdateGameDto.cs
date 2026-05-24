using System.ComponentModel.DataAnnotations;

namespace GameStore.API.Dtos;

public record UpdateGameDto(
    [Required] [StringLength(50, MinimumLength = 3)]
    string Name,
    [Required] [StringLength(20, MinimumLength = 3)]
    string Genre,
    [Range(1, 100)]
    decimal Price,
    [Required]
    DateOnly ReleaseDate
);
