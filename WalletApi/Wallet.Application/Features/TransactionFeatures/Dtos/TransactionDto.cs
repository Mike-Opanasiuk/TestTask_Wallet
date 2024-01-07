namespace Wallet.Application.Features.TransactionFeatures.Dtos;

public class TransactionDto
{
    public string Date { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Sum { get; set; }
    public TransactionCategoryDto Category { get; set; }
}
