using Entities;

namespace Dtos
{
    public record OrderDto
    (
        int OrderId,
        DateOnly? OrderDate,
        decimal OrderSum,
        IEnumerable<OrderItemsDto> OrderItems,
        int UserId
    );
}
