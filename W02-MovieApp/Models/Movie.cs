using System.ComponentModel.DataAnnotations;

namespace MovieApp.Models;

public class Movie
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Genre { get; set; }

    [Display(Name = "Release Year")]
    public int ReleaseYear { get; set; }

    [Range(0, 20)]
    public decimal? Rating { get; set; }

    public string? Director { get; set; }
}
