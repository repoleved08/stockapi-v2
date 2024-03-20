using Api.Data;
using Api.Dtos.Stock;
using Api.Helpers;
using Api.Interface;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Service;

public class StockService(AppDbContext context) : IStockInterface
{
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = context.Stocks.Include(c => c.Comments).ThenInclude(a=>a.AppUser).AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }

        if (!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = query.IsDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }

        var skipNumber = (query.PageNumber - 1) * (query.PageSize);

        return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        var stock = await context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(x => x.Id == id);
        return stock?? null;
    }

    public async Task<Stock?> GetBySymbolAsync(string symbol)
    {
        var stock = await context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        return stock ?? null;
    }

    public async Task<Stock> CreateStockAsync(Stock stock)
    {
        await context.Stocks.AddAsync(stock);
        await context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updateStockRequestDto)
    {
        var stockItem = await context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockItem == null)
        {
            return null;
        }

        stockItem.MarketCap = updateStockRequestDto.MarketCap;
        stockItem.CompanyName = updateStockRequestDto.CompanyName;
        stockItem.Industry = updateStockRequestDto.Industry;
        stockItem.Purchase = updateStockRequestDto.Purchase;
        stockItem.Symbol = updateStockRequestDto.Symbol;
        stockItem.LastDiv = updateStockRequestDto.LastDiv;

        await context.SaveChangesAsync();
        return stockItem;
    }

    public async Task<string> DeleteStockAsync(int id)
    {
        var stockItem = await context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockItem == null)
        {
            return "Item Not Found!";
        }

        context.Stocks.Remove(stockItem);
        await context.SaveChangesAsync();
        return $"Stock Item with id of {id} deleted successfully!";
    }

    public Task<bool> StockExist(int id)
    {
        return context.Stocks.AnyAsync(c => c.Id == id);
    }
}