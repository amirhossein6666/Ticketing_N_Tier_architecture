using Ticketing.DataAccessLayer.Context;
using Ticketing.DataAccessLayer.Entities;
using Ticketing.DataAccessLayer.Interfaces;

namespace Ticketing.DataAccessLayer.Implementations;

public class SupporterRatingRepository: ISupporterRatingRepository
{
    private readonly AppDbContext _appDbContext;

    public SupporterRatingRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<SupporterRating> AddRating(SupporterRating supporterRating)
    {
        _appDbContext.SupporterRatings.Add(supporterRating);
        await _appDbContext.SaveChangesAsync();
        return supporterRating;
    }
}