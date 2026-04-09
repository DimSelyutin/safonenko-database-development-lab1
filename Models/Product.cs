using System.ComponentModel.DataAnnotations;

namespace safonenko.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Article { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Unit { get; set; } = string.Empty;

    [Range(0.001, 100000)]
    public decimal Weight { get; set; }

    [Range(0, int.MaxValue)]
    public decimal MinStock { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
