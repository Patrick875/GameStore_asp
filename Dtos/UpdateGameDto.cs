using System.ComponentModel.DataAnnotations;

namespace GameStore_API.Dtos;

public record class UpdateGameDto
(
    [Required]
    [StringLength(50)]
    string Name,
    int GenreId,
    [Range(1,100)]
    decimal Price,
    DateOnly ReleaseDate
);
