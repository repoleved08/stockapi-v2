using Api.Data;
using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Service;

public class PortfolioService : IPortfolioInterface
{
    private readonly AppDbContext _context;

    public PortfolioService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetUserPortfolio(AppUser user)
    {
        return await _context.Portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Industry = stock.Stock.Industry,
                Purchase = stock.Stock.Purchase,
                LastDiv = stock.Stock.LastDiv,
                MarketCap = stock.Stock.MarketCap
                
            }).ToListAsync();
    }

    public async Task<Portfolio> AddToUserPortfolio(Portfolio portfolio)
    {
        
        await _context.Portfolios.AddAsync(portfolio);
        await _context.SaveChangesAsync();
        return portfolio;
    }
}