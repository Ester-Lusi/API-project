namespace Dtos
{
    public record ProductDto
    (
        int ProductId,
        string ProductName,
        int Price,
        string CategoryName,
        string Description
    );
}
