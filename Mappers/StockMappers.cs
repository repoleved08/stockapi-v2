using Api.Dtos.Comment;
using Api.Dtos.Stock;
using Api.Models;

namespace Api.Mappers;

public static class StockMappers
{
    public static StockDto ToStockDto(this Stock stockModel)
    {
        return new StockDto
        {
            Id = stockModel.Id,
            CompanyName = stockModel.CompanyName,
            Symbol = stockModel.Symbol,
            MarketCap = stockModel.MarketCap,
            Industry = stockModel.Industry,
            LastDiv = stockModel.LastDiv,
            Purchase = stockModel.Purchase,
            Comments = stockModel.Comments?.Select(c=>c.ToCommentFromCommentDto()).ToList() ?? new List<CommentDto>()
        };
    }

    public static Stock ToStockFromCreateRequestDto(this CreateRequestDto createRequestDto)
    {
        return new Stock
        {
            Purchase = createRequestDto.Purchase,
            MarketCap = createRequestDto.MarketCap,
            Industry = createRequestDto.Industry,
            LastDiv = createRequestDto.LastDiv,
            CompanyName = createRequestDto.CompanyName,
            Symbol = createRequestDto.Symbol,

        };
    }
}