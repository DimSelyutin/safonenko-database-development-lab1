using System.ComponentModel.DataAnnotations;

namespace safonenko.Models;

public class Supplier
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(300)]
    public string Details { get; set; } = string.Empty;

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}
