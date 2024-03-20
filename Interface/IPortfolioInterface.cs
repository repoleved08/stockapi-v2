using Api.Models;

namespace Api.Interface;

public interface IPortfolioInterface
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);
    Task<Portfolio> AddToUserPortfolio(Portfolio portfolio);
}