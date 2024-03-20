using Api.Extensions;
using Api.Interface;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/portfolio")]
[ApiController]

public class PortfolioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockInterface _stockInterface;
    private readonly IPortfolioInterface _portfolioInterface;

    public PortfolioController(UserManager<AppUser> userManager, IStockInterface stockInterface, IPortfolioInterface portfolioInterface)
    {
        _userManager = userManager;
        _stockInterface = stockInterface;
        _portfolioInterface = portfolioInterface;
    }

    [HttpGet("portfolio")]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var userName = User.GetByUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var userPortfolio = await _portfolioInterface.GetUserPortfolio(appUser);
        return Ok(userPortfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        var userName = User.GetByUserName();
        var appUser = await _userManager.FindByNameAsync(userName);
        var stock = await _stockInterface.GetBySymbolAsync(symbol);
        if (stock == null)
        {
            return BadRequest("Stock Not Found");
        }
        else
        {
            var userPortfolio = await _portfolioInterface.GetUserPortfolio(appUser);
            if (userPortfolio.Any(e=>e.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Cannot Add same stock to portfolio");
            }
            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };
            await _portfolioInterface.AddToUserPortfolio(portfolio);
            return Created();
        }
    }
}
