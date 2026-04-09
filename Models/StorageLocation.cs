using System.ComponentModel.DataAnnotations;

namespace safonenko.Models;

public class StorageLocation
{
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Row { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Shelf { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Cell { get; set; } = string.Empty;

    public ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();

    public string DisplayName => $"Ряд {Row}, Полка {Shelf}, Ячейка {Cell}";
}
