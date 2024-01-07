using Wallet.Application.Features.TransactionFeatures.Dtos;

namespace Wallet.Application.Features.CardFeatures.Dtos;

public record CardDto
{
    public decimal MaxLimit { get; set; }
    public decimal Balance { get; set; }
    public decimal Available { get; set; }
    public string Points { get; set; }

    public List<TransactionDto> Transactions = new List<TransactionDto>();

    public int TotalTransactionsPages { get; set; }
}
