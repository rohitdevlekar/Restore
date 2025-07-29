namespace API.DTOs;

public class BasketItemDto
{

    public int productId { get; set; }

    public required string Name { get; set; }

    public required long Price { get; set; }

    public required string PictureUrl { get; set; }

    public required string Brand { get; set; }

    public required string Type { get; set; }
    public required int Quantity { get; set; }


}