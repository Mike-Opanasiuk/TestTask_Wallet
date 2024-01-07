namespace Wallet.Application.Features.TransactionFeatures.Dtos;

public class TransactionDto
{
    public DateTime CreatedOn { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public decimal Sum { get; set; }
    public TransactionCategoryDto Category { get; set; }
}
