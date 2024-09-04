using Microsoft.Extensions.Caching.Distributed;
using Basket.API.Entities;

namespace Basket.API.Repositories.Interfaces;

public interface IBasketRepository
{
    Task<Cart?> GetBasketByUsername(string username);
    Task<Cart?> UpdateBasket(Cart cart, DistributedCacheEntryOptions options = null); // ton tai trong vong bao nhieu lau
    Task<bool> DeleteBasketFromUsername(string username);
}