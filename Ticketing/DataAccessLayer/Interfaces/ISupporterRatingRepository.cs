using Ticketing.DataAccessLayer.Entities;
using Ticketing.Dtos.UserDtos;

namespace Ticketing.DataAccessLayer.Interfaces;

public interface ISupporterRatingRepository
{
    public Task<SupporterRating> AddRating(SupporterRating supporterRating);
}