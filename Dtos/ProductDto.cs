namespace Dtos
{
    public record ProductDto
    (
        int ProductId,
        string ProductName,
        int Price,
        CategoryDto CategoryName,
        string Description
    );
}
