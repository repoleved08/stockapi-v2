using Api.Dtos.Stock;
using Api.Helpers;
using Api.Models;

namespace Api.Interface;

public interface IStockInterface
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock?> GetBySymbolAsync(string symbol);
    Task<Stock> CreateStockAsync(Stock stock);
    Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto updateStockRequestDto);
    Task<string> DeleteStockAsync(int id);
    Task<bool> StockExist(int id);
}
