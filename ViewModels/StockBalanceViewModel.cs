namespace safonenko.ViewModels;

public class StockBalanceViewModel
{
    public int ProductId { get; set; }
    public string Article { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public decimal MinStock { get; set; }
    public decimal CurrentStock { get; set; }
}
