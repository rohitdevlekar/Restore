using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class BasketController(StoreContext context) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasket()
    {
        var basket = await RetriveBasket();

        if (basket == null) return NoContent();

        return basket;

    }


    [HttpPost]
    public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
    {
        // get basket
        var basket = await RetriveBasket();

        // create basket
        basket ??= createBasket();

        // get product
        var product = await context.Products.FindAsync(productId);

        if (product == null) return BadRequest("Problem adding item to basket");

        // add item to basket
        basket.AddItem(product , quantity);


        // save changes
        var result = await context.SaveChangesAsync() > 0;

        if (result) return CreatedAtAction(nameof(GetBasket) , basket);


        return BadRequest("Problem updating basket");
    }

    private Basket createBasket()
    {
        var basketId = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions
        {
            IsEssential = true,
            Expires = DateTime.UtcNow.AddDays(30)
        };

        Response.Cookies.Append("basketId", basketId, cookieOptions);
        var basket = new Basket { BasketId = basketId };

        context.Baskets.Add(basket);

        return basket;

    }

    private async Task<Basket?> RetriveBasket()
    {
        return await context.Baskets.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.BasketId == Request.Cookies["basketId"]);

    }

    [HttpDelete]
    public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
    {
        // get basket
        // remove basket
        // save changes

        return Ok();


    }
    
}
