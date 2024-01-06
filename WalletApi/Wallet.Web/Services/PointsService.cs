using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet.Core.Entities;
using Wallet.Infrastructure;
using Wallet.Infrastructure.UnitOfWork.Abstract;

namespace Wallet.Web.Services;

public class PointsService
{
    IUnitOfWork unitOfWork;

    public PointsService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }
    
    public async Task AddPointsAsync()
    {
        var pointToAdd = CalculatePoints(GetDaysInQuarter(DateTime.Now));

        var cardsIds = unitOfWork.Cards.Get().Select(c => c.Id);

        foreach (var id in cardsIds)
        {
            await unitOfWork.Points.InsertAsync(new PointEntity()
            {
                Amount = pointToAdd,
                CardId = id
            });
        }

        await unitOfWork.SaveChangesAsync();
    }


    private static decimal CalculatePoints(int dayInQuarter)
    {
        if (dayInQuarter == 1)
        {
            return 2;
        }
        else if (dayInQuarter == 2)
        {
            return 3;
        }
        else
        {
            var previousDayPoints = CalculatePoints(dayInQuarter - 1);

            return previousDayPoints * 0.6m + previousDayPoints;
        }
    }

    static int GetDaysInQuarter(DateTime date)
    {
        int quarter = (date.Month - 1) / 3 + 1;
        int startMonth = (quarter - 1) * 3 + 1;

        DateTime startOfQuarter = new DateTime(date.Year, startMonth, 1);
        TimeSpan span = date - startOfQuarter;

        return span.Days + 1; // Adding 1 to include the current day
    }
}
