using System.ComponentModel.DataAnnotations;

namespace backend.Dto;

public class CreatePortfolioDto
{
    [Required]
    public string UserId { get; set; }

    [Required]
    public string Name { get; set; }
}