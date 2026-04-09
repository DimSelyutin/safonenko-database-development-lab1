using System.ComponentModel.DataAnnotations;

namespace safonenko.Models;

public class StockMovement
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public StockMovementType OperationType { get; set; }

    [Range(0.001, 1000000)]
    public decimal Quantity { get; set; }

    [Required]
    [StringLength(100)]
    public string DocumentBase { get; set; } = string.Empty;

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public int StorageLocationId { get; set; }
    public StorageLocation? StorageLocation { get; set; }
}
